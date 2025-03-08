using Core.DataStructures.Art;
using Core.Utilities.DataStructures;

namespace MusicGeneration.Models.Markov.Helpers
{
    internal static class AttributeMapper
    {
        private const int pitchMultiplier = 127;
        private const double largeShapeMax =  1.0;
        private const double maxDistanceFromCentre = 0.5;
        private const int minAcceptablePitch = 20;

        public static int GetPitch(ref ObjectAttributes imageAttributes)
        {
            double[] attributesOfInterest = [1 - imageAttributes.Area, imageAttributes.Temperature, imageAttributes.Complexity];

            int pitch = (int)(attributesOfInterest.Average() * pitchMultiplier);

            if (pitch < minAcceptablePitch)
                pitch += 20;

            return pitch;
        }

        public static int GetVelocity(ref ObjectAttributes imageAttributes)
        {
            double value = Math.Max(0, Math.Min(maxDistanceFromCentre, imageAttributes.GetDistanceFromCentre()));

            return (int)((maxDistanceFromCentre - value) * 100) + 30;
        }

        public static double GetNoteLength(ref ObjectAttributes imageAttributes)
        {
            double[] thresholds = [.25, .5, .75, 1.0];
            double[] outputs = [.25, .5, 1.0, 1.5];

            return MapValueToOutput(thresholds, outputs, 1 - imageAttributes.Complexity);
        }

        public static int GetNumberOfNotes(ref ObjectAttributes imageAttributes)
        {
            double[] thresholds = [.2, .4, .6, .8, 1.0];
            int[] outputs = [4, 6, 8, 10, 12];

            return MapValueToOutput(thresholds, outputs, imageAttributes.Complexity);
        }

        public static int GetBPM(ref CanvasAttributes canvasAttributes, int numberOfObjects)
        {
            double avg = canvasAttributes.AreaCovered / numberOfObjects;
            double value = Math.Max(0, Math.Min(largeShapeMax, avg));
            int[] outputs = [140, 130, 120, 115, 110, 100];

            int index = (int)(value * (outputs.Length - largeShapeMax));

            return outputs[index];
        }

        private static T MapValueToOutput<T>(double[] thresholds, T[] outputs, double value)
        {
            for (int i = 0; i < thresholds.Length; i++)
            {
                if (value <= thresholds[i])
                {
                    return outputs[i];
                }
            }

            return outputs[0];
        }
    }
}

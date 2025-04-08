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

        public static int GetPitch(ref ObjectAttributes imageAttributes, ref string feedback)
        {
            double area = imageAttributes.Area;
            double temp = imageAttributes.Temperature;
            double complex = imageAttributes.Complexity;

            double[] attributesOfInterest = [1 - area, temp, complex];

            int pitch = (int)(attributesOfInterest.Average() * pitchMultiplier);

            feedback += $"A combination of {nameof(imageAttributes.Area)}, {nameof(imageAttributes.Temperature)}, and {nameof(imageAttributes.Complexity)} has given a pitch value {pitch}. ";

            if (pitch < minAcceptablePitch)
                pitch += 20;

            return pitch;
        }

        public static int GetVelocity(ref ObjectAttributes imageAttributes)
        {
            double value = Math.Max(0, Math.Min(maxDistanceFromCentre, imageAttributes.GetDistanceFromCentre()));

            return (int)((127 - 80) * (maxDistanceFromCentre - value) / maxDistanceFromCentre) + 80;
        }

        public static double GetNoteLength(ref ObjectAttributes imageAttributes, ref string feedback)
        {
            double[] thresholds = [.25, .5, .75, 1.0];
            double[] outputs = [.25, .5, 1.0, 1.5];

            double result = MapValueToOutput(thresholds, outputs, 1 - imageAttributes.Complexity);

            feedback += $"The object {nameof(imageAttributes.Complexity)} has a score of {imageAttributes.Complexity}, this maps to {result} note length. ";

            return result;
        }

        public static int GetNumberOfNotes(ref ObjectAttributes imageAttributes, ref string feedback)
        {
            double[] thresholds = [.2, .4, .6, .8, 1.0];
            int[] outputs = [4, 6, 8, 10, 12];

            int result = MapValueToOutput(thresholds, outputs, imageAttributes.Complexity);

            feedback += $"I am selecting {result} number of notes for new motif. ";

            return result;
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

using Core.DataStructures.Art;
using Core.DataStructures.Music;

namespace Core.Utilities.DataStructures
{
    public static class Extensions
    {
        public static double GetDistanceTo(this ObjectAttributes current, ObjectAttributes other)
        {
            double xDiff = Math.Pow(current.CanvasLocation.X - other.CanvasLocation.X, 2);
            double yDiff = Math.Pow(current.CanvasLocation.Y -  other.CanvasLocation.Y, 2);

            return Math.Sqrt(xDiff + yDiff);
        }

        public static double GetDistanceFromCentre(this ObjectAttributes current)
        {
            double xDiff = Math.Pow(current.CanvasLocation.X - 0.5, 2);
            double yDiff = Math.Pow(current.CanvasLocation.Y - 0.5, 2);

            return Math.Sqrt(xDiff + yDiff);
        }

        public static (ObjectAttributes closestObject, double distance) GetClosestObject(this List<ObjectAttributes> objectAttributes, ObjectAttributes objectOfInterest)
        {
            double closestDistance = double.MaxValue;
            ObjectAttributes closestObject = objectAttributes.First();

            foreach (ObjectAttributes objectAttribute in objectAttributes)
            {
                double distance = objectOfInterest.GetDistanceTo(objectAttribute);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = objectAttribute;
                }
            }

            return (closestObject, closestDistance);
        }

        public static double GetEndTime(this MusicData musicData)
        {
            return musicData.Notes.Select(x => x.StartTime + x.Duration).Max();
        } 
    }
}

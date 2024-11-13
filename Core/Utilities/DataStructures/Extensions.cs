using Core.DataStructures.Art;

namespace Core.Utilities.DataStructures
{
    public static class Extensions
    {
        public static double GetDistanceTo(this ImageAttributes current, ImageAttributes other)
        {
            double xDiff = Math.Pow(current.CanvasLocation.X - other.CanvasLocation.X, 2);
            double yDiff = Math.Pow(current.CanvasLocation.Y -  other.CanvasLocation.Y, 2);

            return Math.Sqrt(xDiff + yDiff);
        }

        public static double GetDistanceFromCentre(this ImageAttributes current)
        {
            double xDiff = Math.Pow(current.CanvasLocation.X - 0.5, 2);
            double yDiff = Math.Pow(current.CanvasLocation.Y - 0.5, 2);

            return Math.Sqrt(xDiff + yDiff);
        }
    }
}

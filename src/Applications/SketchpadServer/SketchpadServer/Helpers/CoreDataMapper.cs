using Core.DataStructures.Art;
using SketchpadServer.Models.Payloads;

namespace SketchpadServer.Helpers
{
    internal static class CoreDataMapper
    {
        private const double normalizingRGB = 255;
        internal static ObjectAttributes MapToCoreData(ref UpdateShapes updateShapes)
        {
            Shape shape = updateShapes.Payload.Shapes.First();
            (double r, double g, double b ) = HexToRGB(shape.Color);

            return new ObjectAttributes()
            {
                Id = shape.Id,
                Area = shape.Area,
                Complexity = shape.Area / shape.Sides,
                CanvasLocation = (shape.Centre[0], shape.Centre[1]),
                COG = Cog(shape.Points.Select(x => (x[0], x[1])).ToList(), shape.Area),
                ColorR = r / normalizingRGB,
                ColorG = g / normalizingRGB,
                ColorB = b / normalizingRGB,
                Tone = Tone(r, g, b),
                Temperature = Temperature(r / normalizingRGB, b / normalizingRGB),
                Hue = Hue(r / normalizingRGB, g / normalizingRGB, b / normalizingRGB)
            };
        }

        private static (double r, double g, double b) HexToRGB(string hex)
        {
            if (hex.StartsWith('#'))
            {
                hex = hex[1..];
            }

            if (hex.Length != 6)
            {
                throw new ArgumentException("Hex color code must be 6 characters long.");
            }

            double r = (double)Convert.ToInt32(hex[..2], 16);
            double g = (double)Convert.ToInt32(hex.Substring(2, 2), 16);
            double b = (double)Convert.ToInt32(hex.Substring(4, 2), 16);

            return (r, g, b);
        }

        private static double Tone(double r, double g, double b)
        {
            return (0.2126 * r + 0.7152 * g + 0.0722 * b) / normalizingRGB;
        }

        private static double Temperature(double rNormalized, double bNormalized)
        {
            return rNormalized - bNormalized;
        }

        private static double Hue(double rNormalized, double gNormalized, double bNormalized)
        {
            double max = Math.Max(rNormalized, Math.Max(gNormalized, bNormalized));
            double min = Math.Min(rNormalized, Math.Min(gNormalized, bNormalized));
            double delta = max - min;

            double hue = 0;

            if (delta == 0)
            {
                hue = 0; // Gray has no hue
            }
            else if (max == rNormalized)
            {
                hue = (60 * ((gNormalized - bNormalized) / delta) + 360) % 360;
            }
            else if (max == gNormalized)
            {
                hue = (60 * ((bNormalized - rNormalized) / delta) + 120) % 360;
            }
            else if (max == bNormalized)
            {
                hue = (60 * ((rNormalized - gNormalized) / delta) + 240) % 360;
            }

            return hue;
        }

        private static (double x, double y) Cog(List<(double x, double y)> coords, double area)
        {
            if (coords.Count == 1)
            {
                return coords[0];
            }

            if (coords.Count == 2)
            {
                return (coords.Select(point => point.x).Average(), coords.Select(point => point.y).Average());
            }

            double cx = 0;
            double cy = 0;

            for (int i = 0; i < coords.Count - 1; i++)
            {
                var (x, y) = coords[i];
                var next = coords[i + 1];

                double commonFactor = (x * next.y - next.x * y);
                cx += (x + next.x) * commonFactor;
                cy += (y + next.y) * commonFactor;
            }

            // Calculate the final centroid coordinates
            cx *= 1 / (6 * area);
            cy *= 1 / (6 * area);

            return (cx, cy);
        }
    }
}

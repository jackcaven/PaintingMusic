using Core.DataStructures.Art;
using Core.Utilities.Colors;
using Serilog;
using System.Drawing;

namespace CanvasCapture.Classes
{
    public static class ImageDataMinerExtensions
    {
        private const int maxColorChannel = 255;
        private const int maxBigObjectPointCount = 55000;

        public static ObjectAttributes GetObjectAttributes(ref Bitmap bitmap, Image canvasImage, ILogger logger)
        {
            List<Point> points = [];
            int perimeterCount = 0;
            List<Color> colors = [];
            List<double> hues = [];
            List<double> temperatures = [];
            List<double> tones = [];

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);

                    if (!IsWhite(pixel))
                    {
                        points.Add(new(x, y));
                        colors.Add(pixel);
                    }

                    tones.Add(pixel.Tone());
                    temperatures.Add(pixel.Temperature());
                    hues.Add(pixel.Hue());
                }
            }

            logger.Information("Color Pixels: {0}, out of {1}", points.Count, bitmap.Width * bitmap.Height);

            if (points.Count < maxBigObjectPointCount)
            {
                foreach (Point point in points)
                {
                    if (IsPerimeterPoint(points, point))
                    {
                        perimeterCount++;
                    }
                }
            }
            else
            {
                perimeterCount = -1;
            }

            logger.Debug("Perimeter Count: {0}", perimeterCount);

            ObjectAttributes objectAttributes = new()
            {
                Id = Guid.NewGuid().ToString(),
                ColorR = colors.Select(c => (double)c.R / maxColorChannel).Average(),
                ColorG = colors.Select(c => (double)c.G / maxColorChannel).Average(),
                ColorB = colors.Select(c => (double)c.B / maxColorChannel).Average(),
                Temperature = temperatures.Average(),
                Hue = hues.Average(),
                Tone = tones.Average(),
                Area = points.Count / (double)(canvasImage.Height * canvasImage.Width),
                Complexity = perimeterCount == -1 ? 0.01 : (double)perimeterCount / (double)points.Count,
                COG = (points.Select(p => p.X).Average() / bitmap.Width, points.Select(p => p.Y).Average() / bitmap.Height),
                CanvasLocation = GetCanvasLocation(canvasImage),
            };

            return objectAttributes;
        }

        private static bool IsWhite(Color color) => color.R == 255 && color.G == 255 && color.B == 255;

        private static bool IsPerimeterPoint(IEnumerable<Point> points, Point pointOfInterest)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    int neighbourX = pointOfInterest.X + x;
                    int neighbourY = pointOfInterest.Y + y;

                    if (neighbourX < 0 || neighbourY < 0) continue;
                    
                    if (!points.Where(p => p.X == neighbourX && p.Y == neighbourY).Any())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static (double X, double Y) GetCanvasLocation(Image image)
        {
            using Bitmap bitmap = new(image);
            List<Point> points = [];


            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);

                    if (!IsWhite(pixel))
                    {
                        points.Add(new(x, y));
                    }
                }
            }

            return (points.Select(p => p.X).Average() / bitmap.Width, points.Select(p => p.Y).Average() / bitmap.Height);
        }
    }
}

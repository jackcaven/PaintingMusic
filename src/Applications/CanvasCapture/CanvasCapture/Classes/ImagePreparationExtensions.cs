using System.Drawing;

namespace CanvasCapture.Classes
{
    public static class ImagePreparationExtensions
    {
        private const int threshold = 60;

        /// <summary>
        /// Crops the image to the rectangle that is provided
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        public static Image CropToCanvas(this Image image, Rectangle cropArea)
        {
            Bitmap bitmap = new(image);
            return bitmap.Clone(cropArea, bitmap.PixelFormat);
        }

        /// <summary>
        /// Gets the differences between the image and another image
        /// </summary>
        /// <param name="newImage"></param>
        /// <param name="oldImage"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Image SubtractImages(this Image newImage, Image oldImage)
        {
            if (newImage.Width != oldImage.Width || newImage.Height != oldImage.Height)
            {
                throw new ArgumentException("Images must be of the same size.");
            }

            Bitmap newBitmap = new(newImage);
            Bitmap oldBitmap = new(oldImage);
            Bitmap diffBitmap = new(newImage.Width, newImage.Height);

            for (int x = 0; x < newImage.Width; x++)
            {
                for (int y = 0; y < newImage.Height; y++)
                {
                    // Get pixel color from each image
                    Color newPixel = newBitmap.GetPixel(x, y);
                    Color oldPixel = oldBitmap.GetPixel(x, y);

                    // Calculate the absolute difference for each color channel
                    int diffR = Math.Abs(newPixel.R - oldPixel.R);
                    int diffG = Math.Abs(newPixel.G - oldPixel.G);
                    int diffB = Math.Abs(newPixel.B - oldPixel.B);

                    // Calculate the overall intensity difference
                    int intensityDifference = (diffR + diffG + diffB) / 3;

                    if (intensityDifference > threshold)
                    {
                        diffBitmap.SetPixel(x, y, newPixel);
                    }
                    else
                    {
                        // Set differences below the threshold to black (or transparent)
                        diffBitmap.SetPixel(x, y, Color.White);
                    }
                }
            }

            return diffBitmap;
        }

        /// <summary>
        /// Crops an image to an object (i.e. removes surrounding whitespace)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Image CropToObject(this Image input)
        {
            Bitmap image = new(input);

            int width = image.Width;
            int height = image.Height;

            // Create a visited array to track processed pixels
            bool[,] visited = new bool[width, height];

            // List to hold bounding boxes of significant regions
            List<Rectangle> regions = new List<Rectangle>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Skip if the pixel is already visited or is background
                    if (visited[x, y] || IsWhite(image.GetPixel(x, y)))
                        continue;

                    // Perform flood fill to identify the connected region
                    List<Point> regionPixels = new List<Point>();
                    FloodFill(image, x, y, visited, regionPixels);

                    // Ignore small regions
                    if (regionPixels.Count > threshold)
                    {
                        // Calculate bounding box for the region
                        Rectangle boundingBox = GetBoundingBox(regionPixels);
                        regions.Add(boundingBox);
                    }
                }
            }

            // Combine all significant regions into one bounding box
            if (regions.Count == 0)
                return image;

            Rectangle finalBoundingBox = CombineBoundingBoxes(regions);

            // Crop the image to the final bounding box
            return image.Clone(finalBoundingBox, image.PixelFormat);
        }

        /// <summary>
        /// Performs a flood fill to find connected components.
        /// </summary>
        private static void FloodFill(Bitmap image, int startX, int startY, bool[,] visited, List<Point> regionPixels)
        {
            int width = image.Width;
            int height = image.Height;
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(startX, startY));

            while (stack.Count > 0)
            {
                Point p = stack.Pop();

                if (p.X < 0 || p.Y < 0 || p.X >= width || p.Y >= height || visited[p.X, p.Y] || IsWhite(image.GetPixel(p.X, p.Y)))
                    continue;

                visited[p.X, p.Y] = true;
                regionPixels.Add(p);

                stack.Push(new Point(p.X + 1, p.Y));
                stack.Push(new Point(p.X - 1, p.Y));
                stack.Push(new Point(p.X, p.Y + 1));
                stack.Push(new Point(p.X, p.Y - 1));
            }
        }

        /// <summary>
        /// Calculates the bounding box for a set of points.
        /// </summary>
        private static Rectangle GetBoundingBox(List<Point> points)
        {
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;

            foreach (Point p in points)
            {
                if (p.X < minX) minX = p.X;
                if (p.Y < minY) minY = p.Y;
                if (p.X > maxX) maxX = p.X;
                if (p.Y > maxY) maxY = p.Y;
            }

            return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        /// <summary>
        /// Combines multiple rectangles into a single bounding box.
        /// </summary>
        private static Rectangle CombineBoundingBoxes(List<Rectangle> boxes)
        {
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;

            foreach (var box in boxes)
            {
                if (box.Left < minX) minX = box.Left;
                if (box.Top < minY) minY = box.Top;
                if (box.Right > maxX) maxX = box.Right;
                if (box.Bottom > maxY) maxY = box.Bottom;
            }

            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// Determines if a color is white (background detection).
        /// </summary>
        private static bool IsWhite(Color color)
        {
            return color.R > 240 && color.G > 240 && color.B > 240;
        }
    }
}

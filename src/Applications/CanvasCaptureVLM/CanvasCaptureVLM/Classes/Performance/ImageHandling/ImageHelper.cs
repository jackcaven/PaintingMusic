using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace CanvasCaptureVLM.Classes.Performance.ImageHandling
{
    internal static class ImageHelper
    {
        private const int threshold = 60;

        /// <summary>
        /// Takes image and reduces size whilst maintaining asepct ratio
        /// </summary>
        /// <param name="image"></param>
        public static void ReduceImageSize(ref Bitmap image)
        {
            ArgumentNullException.ThrowIfNull(image);

            int newWidth = image.Width / 2;
            int newHeight = image.Height / 2;

            using Bitmap resizedImage = new(newWidth, newHeight, image.PixelFormat);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            image.Dispose();
            image = new Bitmap(resizedImage);
        }

        /// <summary>
        /// Crops the image to the rectangle that is provided
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        public static void CropToCanvas(ref Bitmap image, Rectangle cropArea)
        {
            ArgumentNullException.ThrowIfNull(image);

            if (cropArea.Width <= 0 || cropArea.Height <= 0 || cropArea.X < 0 || cropArea.Y < 0 ||
                cropArea.Right > image.Width || cropArea.Bottom > image.Height)
            {
                throw new ArgumentException("Invalid crop area.");
            }

            // Create a new bitmap with the crop size
            using Bitmap croppedBitmap = new(cropArea.Width, cropArea.Height, image.PixelFormat);

            using (Graphics g = Graphics.FromImage(croppedBitmap))
            {
                g.DrawImage(image, new Rectangle(0, 0, cropArea.Width, cropArea.Height), cropArea, GraphicsUnit.Pixel);
            }

            image.Dispose();
            image = new Bitmap(croppedBitmap);
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

        public static BinaryData ImageToBinaryData(Image image, ImageFormat format)
        {
            using MemoryStream ms = new();
            image.Save(ms, format);
            return BinaryData.FromBytes(ms.ToArray());
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

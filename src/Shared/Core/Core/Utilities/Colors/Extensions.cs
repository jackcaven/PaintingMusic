using System.Drawing;

namespace Core.Utilities.Colors
{
    public static class Extensions
    {
        private const int maxColorChannelValue = 255;

        public static double Tone(this Color color) => (0.2126 * (double)color.R + 0.7152 * (double)color.G + 0.0722 * (double)color.B) / maxColorChannelValue;

        public static double Temperature(this Color color) => Math.Abs(((double)color.R - (double)color.B) / maxColorChannelValue);

        public static double Hue(this Color color)
        {
            double rNormalized = (double)color.R / maxColorChannelValue;
            double bNormalized = (double)color.B / maxColorChannelValue;
            double gNormalized = (double)color.G / maxColorChannelValue;

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

    }
}

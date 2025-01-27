using Core.Utilities.Colors;
using System.Drawing;

namespace CoreTests.Utilities.Colors
{
    public class ExtensionsTests
    {
        [Fact]
        public void Tone_CalculatesCorrectly()
        {
            // Arrange
            var color = Color.FromArgb(255, 100, 150, 200);
            double expectedTone = (0.2126 * 100 + 0.7152 * 150 + 0.0722 * 200) / 255;

            // Act
            double actualTone = color.Tone();

            // Assert
            Assert.Equal(expectedTone, actualTone, precision: 5);
        }

        [Fact]
        public void Temperature_CalculatesCorrectly()
        {
            // Arrange
            var color = Color.FromArgb(255, 200, 150, 100);
            double expectedTemperature = (200 - 100) / 255.0;

            // Act
            double actualTemperature = color.Temperature();

            // Assert
            Assert.Equal(expectedTemperature, actualTemperature, precision: 5);
        }

        [Theory]
        [InlineData(255, 0, 0, 0)] // Pure red
        [InlineData(0, 255, 0, 120)] // Pure green
        [InlineData(0, 0, 255, 240)] // Pure blue
        [InlineData(255, 255, 255, 0)] // White (no hue)
        [InlineData(0, 0, 0, 0)] // Black (no hue)
        public void Hue_CalculatesCorrectly(int r, int g, int b, double expectedHue)
        {
            // Arrange
            var color = Color.FromArgb(255, r, g, b);

            // Act
            double actualHue = color.Hue();

            // Assert
            Assert.Equal(expectedHue, actualHue, precision: 5);
        }

        [Fact]
        public void Hue_HandlesComplexColors()
        {
            // Arrange
            var color = Color.FromArgb(255, 123, 234, 45);
            double expectedHue = 95.238; // Precomputed expected hue for this color

            // Act
            double actualHue = color.Hue();

            // Assert
            Assert.Equal(expectedHue, actualHue, precision: 2);
        }
    }
}

using SketchpadServer.Helpers;
using SketchpadServerTests.Mocks;

namespace SketchpadServerTests.Helpers
{
    public class CoreDataMapperTests
    {
        [Fact]
        public void MapToCoreData_ValidInput_ReturnsExpectedObjectAttributes()
        {
            // Arrange
            var updateShapes = MockUpdateShapes.CreateValidUpdateShapes();
            var expectedShape = updateShapes.Payload.Shapes.First();
            var expectedColor = CoreDataMapper.HexToRGB(expectedShape.Color);

            // Act
            var result = CoreDataMapper.MapToCoreData(ref updateShapes);

            // Assert
            Assert.Equal(expectedShape.Id, result.Id);
            Assert.Equal(expectedShape.Area, result.Area);
            Assert.Equal(expectedShape.Area / expectedShape.Sides, result.Complexity);
            Assert.Equal((expectedShape.Centre[0], expectedShape.Centre[1]), result.CanvasLocation);
            Assert.Equal(expectedColor.r / 255, result.ColorR, precision: 5);
            Assert.Equal(expectedColor.g / 255, result.ColorG, precision: 5);
            Assert.Equal(expectedColor.b / 255, result.ColorB, precision: 5);
            Assert.Equal(CoreDataMapper.Tone(expectedColor.r, expectedColor.g, expectedColor.b), result.Tone, precision: 5);
            Assert.Equal(CoreDataMapper.Temperature(expectedColor.r / 255, expectedColor.b / 255), result.Temperature, precision: 5);
            Assert.Equal(CoreDataMapper.Hue(expectedColor.r / 255, expectedColor.g / 255, expectedColor.b / 255), result.Hue, precision: 5);
        }

        [Fact]
        public void HexToRGB_ValidHex_ReturnsCorrectRGB()
        {
            // Arrange
            var hex = "#FF5733"; // Example color
            var expected = (r: 255.0, g: 87.0, b: 51.0);

            // Act
            var result = CoreDataMapper.HexToRGB(hex);

            // Assert
            Assert.Equal(expected.r, result.r);
            Assert.Equal(expected.g, result.g);
            Assert.Equal(expected.b, result.b);
        }

        [Fact]
        public void HexToRGB_InvalidHex_ThrowsArgumentException()
        {
            // Arrange
            var invalidHex = "#XYZ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => CoreDataMapper.HexToRGB(invalidHex));
        }

        [Fact]
        public void Tone_ValidRGB_ReturnsExpectedTone()
        {
            // Arrange
            double r = 100, g = 150, b = 200;

            // Act
            var result = CoreDataMapper.Tone(r, g, b);

            // Assert
            Assert.Equal(0.2126 * r + 0.7152 * g + 0.0722 * b, result * 255, precision: 5);
        }

        [Fact]
        public void Temperature_ValidNormalizedValues_ReturnsExpectedDifference()
        {
            // Arrange
            double rNormalized = 0.8, bNormalized = 0.4;

            // Act
            var result = CoreDataMapper.Temperature(rNormalized, bNormalized);

            // Assert
            Assert.Equal(0.8 - 0.4, result, precision: 5);
        }

        [Fact]
        public void Hue_ValidNormalizedValues_ReturnsExpectedHue()
        {
            // Arrange
            double r = 1, g = 0.5, b = 0.2;

            // Act
            var result = CoreDataMapper.Hue(r, g, b);

            // Assert
            Assert.InRange(result, 0, 360); // Hue is always between 0 and 360 degrees
        }

        [Fact]
        public void Cog_ValidCoordinates_ReturnsExpectedCog()
        {
            // Arrange
            var coords = new List<(double x, double y)>
            {
                (0, 0),
                (4, 0),
                (4, 3),
                (0, 3),
                (0, 0)
            };
            double area = 12; // Area of the rectangle

            // Act
            var result = CoreDataMapper.Cog(coords, area);

            // Assert
            Assert.Equal((2.0, 1.5), result);
        }

        [Fact]
        public void Cog_SinglePoint_ReturnsPointItself()
        {
            // Arrange
            var coords = new List<(double x, double y)> { (3.0, 4.0) };
            double area = 1;

            // Act
            var result = CoreDataMapper.Cog(coords, area);

            // Assert
            Assert.Equal((3.0, 4.0), result);
        }

        [Fact]
        public void Cog_TwoPoints_ReturnsMidpoint()
        {
            // Arrange
            var coords = new List<(double x, double y)> { (0, 0), (4, 4) };
            double area = 1;

            // Act
            var result = CoreDataMapper.Cog(coords, area);

            // Assert
            Assert.Equal((2.0, 2.0), result);
        }
    }
}

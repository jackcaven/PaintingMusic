using Core.BaseClasses;
using Core.DataStructures.Art;
using Core.DataStructures.Music;

namespace CoreTests.BaseClasses
{
    public class TestMusicGeneratorConfiguration : MusicGeneratorConfiguration
    {
    }

    public class MusicGeneratorConfigurationTests
    {
        [Fact]
        public void AvailableImageAttributes_ShouldContainExpectedAttributes()
        {
            // Arrange
            var testConfig = new TestMusicGeneratorConfiguration();

            // Act
            var availableAttributes = testConfig.AvailableImageAttributes;

            // Assert
            var expectedAttributes = new List<string>
            {
                "Id",
                "ColorR",
                "ColorG",
                "ColorB",
                "Temperature",
                "Hue",
                "Tone",
                "Area",
                "Complexity",
                "CanvasLocation",
                "COG"
            };
            Assert.Equal(expectedAttributes, availableAttributes);
        }

        [Fact]
        public void AvailableMusicAttributes_ShouldContainExpectedAttributes()
        {
            // Arrange
            var testConfig = new TestMusicGeneratorConfiguration();

            // Act
            var availableAttributes = testConfig.AvailableMusicAttributes;

            // Assert
            var expectedAttributes = new List<string>
            {
                "Pitch",
                "Velocity",
                "NoteLength",
                "NumberOfNotes",
                "BPM"
            };
            Assert.Equal(expectedAttributes, availableAttributes);
        }

        [Fact]
        public void GetAvailableAttributes_ShouldReturnCorrectPropertyNames()
        {
            // Arrange
            var testConfig = new TestMusicGeneratorConfiguration();

            // Act
            var objectAttributes = testConfig.GetType().BaseType.GetMethod("GetAvailableAttributes", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var imageAttributes = (IEnumerable<string>)objectAttributes.Invoke(null, new object[] { typeof(ObjectAttributes) });
            var musicAttributes = (IEnumerable<string>)objectAttributes.Invoke(null, new object[] { typeof(MusicAttributes) });

            // Assert
            Assert.Contains("Id", imageAttributes);
            Assert.Contains("ColorR", imageAttributes);
            Assert.Contains("Pitch", musicAttributes);
            Assert.Contains("BPM", musicAttributes);
        }
    }
}

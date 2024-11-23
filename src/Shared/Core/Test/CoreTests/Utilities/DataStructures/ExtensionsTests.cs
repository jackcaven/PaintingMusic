using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.Utilities.DataStructures;

namespace CoreTests.Utilities.DataStructures
{
    public class ExtensionsTests
    {
        [Fact]
        public void GetDistanceTo_ReturnsCorrectDistance()
        {
            // Arrange
            ObjectAttributes obj1 = new() { COG = (0, 0), CanvasLocation = (0, 0) };
            ObjectAttributes obj2 = new() { COG = (0, 0), CanvasLocation = (3, 4) };

            // Act
            double result = obj1.GetDistanceTo(obj2);

            // Assert
            Assert.Equal(5, result); // Expected distance is 5 (3-4-5 triangle).
        }

        [Fact]
        public void GetDistanceFromCentre_ReturnsCorrectDistance()
        {
            // Arrange
            ObjectAttributes obj = new() { COG = (0, 0), CanvasLocation = (0.5, 0.5) };

            // Act
            double result = obj.GetDistanceFromCentre();

            // Assert
            Assert.Equal(0, result); // At the center, the distance is 0.
        }

        [Fact]
        public void GetDistanceFromCentre_CalculatesCorrectly_ForNonCenterLocation()
        {
            // Arrange
            ObjectAttributes obj = new() { COG = (0, 0), CanvasLocation = (1.0, 1.0) };

            // Act
            double result = obj.GetDistanceFromCentre();

            // Assert
            double expected = Math.Sqrt(Math.Pow(1.0 - 0.5, 2) + Math.Pow(1.0 - 0.5, 2));
            Assert.Equal(expected, result, precision: 6);
        }

        [Fact]
        public void GetClosestObject_ReturnsClosestObjectAndDistance()
        {
            // Arrange
            var objOfInterest = new ObjectAttributes { COG = (0, 0), CanvasLocation = (0, 0) };
            var objects = new List<ObjectAttributes>
        {
            new() { COG = (0, 0), CanvasLocation = (3, 4) },
            new() { COG = (0, 0), CanvasLocation = (1, 1) },
            new() { COG = (0, 0), CanvasLocation = (5, 5) }
        };

            // Act
            var (closestObject, distance) = objects.GetClosestObject(objOfInterest);

            // Assert
            var expectedClosest = objects[1]; // (1, 1) is the closest.
            double expectedDistance = Math.Sqrt(2); // Distance to (1, 1).
            Assert.Equal(expectedClosest, closestObject);
            Assert.Equal(expectedDistance, distance, precision: 6);
        }

        [Fact]
        public void GetClosestObject_HandlesSingleObject()
        {
            // Arrange
            var objOfInterest = new ObjectAttributes { COG = (0, 0), CanvasLocation = (0, 0) };
            var objects = new List<ObjectAttributes>
        {
            new() { COG = (0, 0), CanvasLocation = (3, 4) }
        };

            // Act
            var (closestObject, distance) = objects.GetClosestObject(objOfInterest);

            // Assert
            Assert.Equal(objects[0], closestObject);
            Assert.Equal(5, distance); // 3-4-5 triangle.
        }

        [Fact]
        public void GetClosestObject_HandlesEmptyList_ThrowsException()
        {
            // Arrange
            ObjectAttributes objOfInterest = new() { COG = (0, 0), CanvasLocation = (0, 0) };
            List<ObjectAttributes> objects = [];

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => objects.GetClosestObject(objOfInterest));
        }

        [Fact]
        public void GetEndTime_ReturnsCorrectEndTime_ForValidMusicData()
        {
            // Arrange
            MusicData musicData = new ()
            {
                Instrument = "Piano",
                BPM = 120,
                Notes =
                [
                    new() { Notes = [60], Velocity = 100, StartTime = 0.0, Duration = 1.0 },
                    new() { Notes = [62], Velocity = 90, StartTime = 2.0, Duration = 0.5 },
                    new() { Notes = [64], Velocity = 80, StartTime = 1.5, Duration = 0.75 }
                ]
            };

            // Act
            double result = musicData.GetEndTime();

            // Assert
            // The last note starts at 2.0 and has a duration of 0.5, so the expected end time is 2.5.
            Assert.Equal(2.5, result, precision: 6);
        }

        [Fact]
        public void GetEndTime_HandlesSingleNoteCorrectly()
        {
            // Arrange
            MusicData musicData = new ()
            {
                Instrument = "Guitar",
                BPM = 100,
                Notes =
                [
                    new Note { Notes = [50], Velocity = 70, StartTime = 1.0, Duration = 2.0 }
                ]
            };

            // Act
            double result = musicData.GetEndTime();

            // Assert
            // The single note starts at 1.0 and has a duration of 2.0, so the expected end time is 3.0.
            Assert.Equal(3.0, result, precision: 6);
        }

        [Fact]
        public void GetEndTime_ReturnsZero_WhenNoNotes()
        {
            // Arrange
            MusicData musicData = new ()
            {
                Instrument = "Violin",
                BPM = 90,
                Notes = [] // Empty list of notes
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => musicData.GetEndTime());
        }

        [Fact]
        public void GetEndTime_CalculatesCorrectly_WithMultipleNotesSameStartTime()
        {
            // Arrange
            MusicData musicData = new ()
            {
                Instrument = "Flute",
                BPM = 110,
                Notes =
            [
                new Note { Notes = [60], Velocity = 50, StartTime = 1.0, Duration = 1.5 },
                new Note { Notes = [62], Velocity = 60, StartTime = 1.0, Duration = 2.0 }
            ]
            };

            // Act
            double result = musicData.GetEndTime();

            // Assert
            Assert.Equal(3.0, result, precision: 6);
        }
    }
}

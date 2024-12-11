using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.Interfaces;
using Moq;
using SketchpadServer.Classes;
using SketchpadServer.Helpers;
using SketchpadServerTests.Mocks;

namespace SketchpadServerTests.Classes
{
    public class CoreAdapterTests
    {
        private readonly Mock<ICoreMusicProducer> _mockCoreMusicProducer;
        private readonly CoreAdapter _coreAdapter;

        public CoreAdapterTests()
        {
            _mockCoreMusicProducer = new Mock<ICoreMusicProducer>();
            _coreAdapter = new CoreAdapter(_mockCoreMusicProducer.Object);
        }

        [Fact]
        public void GenerateMusic_ValidInput_AddsObjectAttributesAndUpdatesCanvas()
        {
            // Arrange
            var updateShapes = MockUpdateShapes.CreateValidUpdateShapes();
            var objectAttributes = CoreDataMapper.MapToCoreData(ref updateShapes);
            _mockCoreMusicProducer
                .Setup(x => x.Add(It.IsAny<ObjectAttributes>(), It.IsAny<CanvasAttributes>()))
                .Returns(new MusicData() 
                { 
                    Instrument = "Piano",
                    BPM = 120,
                    Notes = []
                });

            // Act
            var result = _coreAdapter.GenerateMusic(updateShapes);

            // Assert
            Assert.NotNull(result);
            _mockCoreMusicProducer.Verify(x => x.Add(It.IsAny<ObjectAttributes>(), It.IsAny<CanvasAttributes>()), Times.Once);
        }

        [Fact]
        public void DeleteShape_ValidShape_RemovesFromCacheAndUpdatesCanvas()
        {
            // Arrange
            var updateShapes = MockUpdateShapes.CreateValidUpdateShapes();
            var objectAttributes = CoreDataMapper.MapToCoreData(ref updateShapes);
            _coreAdapter.GenerateMusic(updateShapes); // Add a shape to cache

            // Act
            _coreAdapter.DeleteShape(objectAttributes.Id);

            // Assert
            _mockCoreMusicProducer.Verify(x => x.Remove(It.IsAny<ObjectAttributes>(), It.IsAny<CanvasAttributes>()), Times.Once);
        }

        [Fact]
        public void DeleteShape_InvalidShapeID_DoesNotThrow()
        {
            // Act & Assert
            var exception = Record.Exception(() => _coreAdapter.DeleteShape("non-existent-id"));
            _mockCoreMusicProducer.Verify(x => x.Remove(It.IsAny<ObjectAttributes>(), It.IsAny<CanvasAttributes>()), Times.Never);
        }

        [Fact]
        public void Clear_EmptiesCacheAndResetsCanvas()
        {
            // Arrange
            var updateShapes = MockUpdateShapes.CreateValidUpdateShapes();
            _coreAdapter.GenerateMusic(updateShapes); // Add a shape to cache

            // Act
            _coreAdapter.Clear();

            // Assert
            _mockCoreMusicProducer.Verify(x => x.Clear(), Times.Once);
        }
    }
}

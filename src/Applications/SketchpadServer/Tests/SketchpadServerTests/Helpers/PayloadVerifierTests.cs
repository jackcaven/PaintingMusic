using SketchpadServer.Enums;
using SketchpadServer.Helpers;
using SketchpadServerTests.Mocks;

namespace SketchpadServerTests.Helpers
{
    public class PayloadVerifierTests
    {
        [Fact]
        public void VerifyPayload_ValidPayload_ReturnsTrue()
        {
            // Arrange
            var updateShapes = MockUpdateShapes.CreateValidUpdateShapes();

            // Act
            var result = PayloadVerifier.VerifyPayload(updateShapes);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPayload_InvalidCommand_ReturnsFalse()
        {
            // Arrange
            var updateShapes = MockUpdateShapes.CreateInvalidUpdateShapes();

            // Act
            var result = PayloadVerifier.VerifyPayload(updateShapes);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPayload_InvalidSender_ReturnsFalse()
        {
            // Arrange
            var updateShapes = MockUpdateShapes.CreateInvalidUpdateShapes();

            // Act
            var result = PayloadVerifier.VerifyPayload(updateShapes);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetCommand_ValidCommand_ReturnsExpectedCommand()
        {
            // Arrange
            var command = "resetCommand";

            // Act & Assert
            Assert.True(PayloadVerifier.GetCommand(command, out Command resultCommand));

            // Assert
            Assert.Equal(Command.Reset, resultCommand);
        }

        [Fact]
        public void GetCommand_InvalidCommand_ReturnsFalse()
        {
            // Arrange
            var command = "invalidCommand";

            // Act & Assert
            Assert.False(PayloadVerifier.GetCommand(command, out Command _));
        }

        [Theory]
        [InlineData("updateShapes", Command.Update)]
        [InlineData("removeShape", Command.Remove)]
        [InlineData("aiCommand", Command.Ai)]
        public void GetCommand_ValidCommands_ReturnsExpectedResults(string command, Command expectedCommand)
        {
            // Act
            Assert.True(PayloadVerifier.GetCommand(command, out Command resultCommand));

            // Assert
            Assert.Equal(expectedCommand, resultCommand);
        }

        [Theory]
        [InlineData("")]
        [InlineData("unknownCommand")]
        public void GetCommand_InvalidCommands_ThrowsArgumentException(string command)
        {
            // Act & Assert
            Assert.False(PayloadVerifier.GetCommand(command, out _));
            
        }
    }
}

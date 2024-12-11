using SketchpadServer.Enums;
using SketchpadServer.Models.Payloads;

namespace SketchpadServer.Helpers
{
    public static class PayloadVerifier
    {
        private const string validSender = "front-end";

        private static readonly Dictionary<string, Command> Commands = new()
        {
            { "resetCommand", Command.Reset },
            { "updateShapes", Command.Update },
            { "removeShape" , Command.Remove },
            { "transposeCommand" , Command.Transpose },
            { "aiCommand" , Command.Ai },
            { "pruningCommand" , Command.Pruning },
            { "transportCommand" , Command.Transport },
            { "killShape" , Command.Kill },
            { "mute", Command.Mute },
        };

        public static bool VerifyPayload(UpdateShapes updateShapes)
        {
            return VerifyCommand(updateShapes.Command) && updateShapes.Sender == validSender;
        }

        public static bool GetCommand(string commandString, out Command command)
        {
            return Commands.TryGetValue(commandString, out command);
        }

        private static bool VerifyCommand(string command) => Commands.ContainsKey(command);
    }
}

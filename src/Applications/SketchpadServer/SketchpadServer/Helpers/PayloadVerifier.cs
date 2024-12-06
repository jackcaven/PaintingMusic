using SketchpadServer.Enums;
using SketchpadServer.Models.Payloads;

namespace SketchpadServer.Helpers
{
    internal static class PayloadVerifier
    {
        private const string validSender = "front-end";

        public static Dictionary<string, Command> Commands = new()
        {
            { "resetCommand", Command.Reset },
            { "updateShapes", Command.Update },
            { "removeShape" , Command.Remove },
            { "transposeCommand" , Command.Transpose },
            { "aiCommand" , Command.Ai },
            { "pruningCommand" , Command.Pruning },
            { "transportCommand" , Command.Transport },
            { "killShape" , Command.Kill }
        };

        public static bool VerifyPayload(UpdateShapes updateShapes)
        {
            return VerifyCommand(updateShapes.Command) && updateShapes.Sender == validSender;
        }

        private static bool VerifyCommand(string command) => Commands.ContainsKey(command);
    }
}

using System.Runtime.CompilerServices;

namespace CanvasCaptureVLM.Classes.Logging
{
    public static class Log
    {
        public static void Debug(string message, [CallerFilePath] string className = "") =>
            LogManager.Write(LogLevel.Debug, message, className);

        public static void Info(string message, [CallerFilePath] string className = "") =>
            LogManager.Write(LogLevel.Info, message, className);

        public static void Warning(string message, [CallerFilePath] string className = "") =>
            LogManager.Write(LogLevel.Warning, message, className);

        public static void Error(string message, [CallerFilePath] string className = "") =>
            LogManager.Write(LogLevel.Error, message, className);
    }
}

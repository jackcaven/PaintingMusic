using CanvasCaptureVLM.Classes.Helper;

namespace CanvasCaptureVLM.Classes.Logging
{
    internal static class LogManager
    {
        private static bool logToFile;
        private static FileLogger? fileLogger;

        internal static event Action<LogEntry>? OnLogReceived;

        internal static void ConfigureFileLogging(bool enableFileLogging)
        {
            logToFile = enableFileLogging;

            if (logToFile && fileLogger == null)
            {
                string logDirectory = Path.Combine(
                    DirectoryHelper.GetAppDataDirectory(),
                    "Logs");

                fileLogger = new FileLogger(logDirectory);
            }
            else if (!logToFile && fileLogger != null)
            {
                fileLogger.Dispose();
                fileLogger = null;
            }
        }

        public static void Write(
            LogLevel level,
            string message,
            string filePath)
        {
            string className = Path.GetFileNameWithoutExtension(filePath);

            var entry = new LogEntry(level, message, className);

            OnLogReceived?.Invoke(entry);

            if (logToFile)
            {
                fileLogger?.Write(entry);
            }
        }

    }
}

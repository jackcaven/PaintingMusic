namespace CanvasCaptureVLM.Classes.Logging
{
    internal class FileLogger : IDisposable
    {
        private readonly string LogFilePath;
        private readonly object Lock = new();

        internal FileLogger(string logDirectory)
        {
            Directory.CreateDirectory(logDirectory);

            var fileName = $"CanvasCaptureVLM_Log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            LogFilePath = Path.Combine(logDirectory, fileName);
        }

        internal void Write(LogEntry entry)
        {
            var logLine = entry.ToString();
            lock (Lock)
            {
                File.AppendAllText(LogFilePath, logLine + Environment.NewLine);
            }
        }

        public void Dispose()
        {
            // No unmanaged resources to dispose
        }
    }
}

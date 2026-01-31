using System;
using System.Collections.Generic;
using System.Text;

namespace CanvasCaptureVLM.Classes.Logging
{
    internal class LogEntry(
        LogLevel level,
        string message,
        string className)
    {
        public DateTime Timestamp { get; } = DateTime.Now;
        public LogLevel Level { get; } = level;
        public string Message { get; } = message;
        public string ClassName { get; } = className;

        public override string ToString()
        {
            return $"{Timestamp:HH:mm:ss} [{Level}] [{ClassName}] {Message}";
        }
    }
}

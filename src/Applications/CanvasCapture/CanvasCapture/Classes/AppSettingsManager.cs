namespace CanvasCapture.Classes
{
    public static class AppSettingsManager
    {
        private const string imgDirEnvVarKey = "PaintingMusicCanvasCaptureImageDirectory";
        private const string simulationDirEnvVarKey = "PaintingMusicCanvasCaptureSimImageDirectory";
        private static readonly string defaultImageDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private static readonly Dictionary<string, string> EnvVarKeys = new()
        {
            { "Performance", imgDirEnvVarKey },
            { "Simulation", simulationDirEnvVarKey }
        };

        public static string GetImageDirectory(string key) 
        {
            if (!EnvVarKeys.TryGetValue(key, out string? value))
                return defaultImageDirectory;

            string directory = Environment.GetEnvironmentVariable(value) ?? string.Empty;

            if (string.IsNullOrEmpty(directory))
            {
                directory = defaultImageDirectory;
                Environment.SetEnvironmentVariable(imgDirEnvVarKey, directory);
            }

            return directory;
        }
        
    }
}

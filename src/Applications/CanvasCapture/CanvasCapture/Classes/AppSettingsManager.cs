namespace CanvasCapture.Classes
{
    public static class AppSettingsManager
    {
        private const string imgDirEnvVarKey = "PaintingMusicCanvasCaptureImageDirectory";
        private static readonly string defaultImageDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string GetImageDirectory() 
        {
            string directory = Environment.GetEnvironmentVariable(imgDirEnvVarKey) ?? string.Empty;

            if (string.IsNullOrEmpty(directory))
            {
                directory = defaultImageDirectory;
                Environment.SetEnvironmentVariable(imgDirEnvVarKey, directory);
            }

            return directory;
        }
        
    }
}

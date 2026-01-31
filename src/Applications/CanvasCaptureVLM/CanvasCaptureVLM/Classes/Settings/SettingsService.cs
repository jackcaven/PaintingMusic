namespace CanvasCaptureVLM.Classes.Settings
{
    internal class SettingsService
    {
        private readonly string defaultImageDirectoryFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public ApplicationSettings Settings { get; } = new ApplicationSettings();
        
        internal SettingsService()
        {
            InitializeImageDirectory();
            GetSecureSettings();
        }

        private void InitializeImageDirectory()
        {
            string persistedImageDirectory = PersistedSettingsManager.GetSetting(SettingsKeys.ImageDirectory);
            
            if (!string.IsNullOrEmpty(persistedImageDirectory))
            {
                Settings.ImageDirectory = defaultImageDirectoryFilePath;
            }
        }

        private void GetSecureSettings()
        {
            var apiKey = SecureSettings.SecureSettingsHandler.LoadSecureSetting("api_key");
            var studelPassword = SecureSettings.SecureSettingsHandler.LoadSecureSetting("strudel_password");
            Settings.APIKey = apiKey;
            Settings.StrudelPassword = studelPassword;
        }
    }
}

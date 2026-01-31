using CanvasCaptureVLM.Classes.Logging;
using CanvasCaptureVLM.Classes.Settings.SecureSettings;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CanvasCaptureVLM.Classes.Settings
{
    public class ApplicationSettings() : INotifyPropertyChanged
    {
        #region Constants
        internal string ApplicationVersion { get; } = "v1.0.0";
        #endregion

        #region Private
        // Application Settings
        private string _ImageDirectory = string.Empty;
        private string _APIKey = string.Empty;
        private string _StrudelEmail = "test@test.com";
        private string _StrudelPassword = string.Empty;
        // Developer Settings
        private bool _ShowLogs = true;
        private bool _SaveLogs = true;
        private bool _SendPrompt = true;
        // Performance Settings
        private string _MusicGenre = string.Empty;
        #endregion

        #region Public
        // Application Settings
        internal string ImageDirectory
        {
            get => _ImageDirectory;
            set { _ImageDirectory = value; OnPropertyChanged(); }
        }

        internal string APIKey
        {
            get => _APIKey;
            set { _APIKey = value; OnPropertyChanged(); }
        }

        internal string StrudelEmail
        {
            get => _StrudelEmail;
            set { _StrudelEmail = value; OnPropertyChanged(); }
        }

        internal string StrudelPassword
        {
            get => _StrudelPassword;
            set { _StrudelPassword = value; OnPropertyChanged(); }
        }

        // Developer Settings
        internal bool ShowLogs
        {
            get => _ShowLogs;
            set { _ShowLogs = value; OnPropertyChanged(); }
        }

        internal bool SaveLogs
        {
            get => _SaveLogs;
            set { _SaveLogs = value; OnPropertyChanged(); }
        }
        internal bool SendPrompt
        {
            get => _SendPrompt;
            set { _SendPrompt = value; OnPropertyChanged(); }
        }
        // Performance Settings
        internal string MusicGenre
        {
            get => _MusicGenre;
            set { _MusicGenre = value; OnPropertyChanged(); }
        }
        #endregion

        #region Property Changed Implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == nameof(ApplicationSettings.SaveLogs))
            {
                LogManager.ConfigureFileLogging(_SaveLogs);
                Log.Debug(propertyName + " changed to " + _SaveLogs, nameof(ApplicationSettings));
            }

            if (propertyName == nameof(ApplicationSettings.APIKey))
            {
                Log.Debug("API Key changed.", nameof(ApplicationSettings));
                SecureSettingsHandler.SaveSecureSetting("api_key", _APIKey);
            }

            if (propertyName == nameof(ApplicationSettings.StrudelPassword))
            {
                Log.Debug("Strudel Password changed.", nameof(ApplicationSettings));
                SecureSettingsHandler.SaveSecureSetting("strudel_password", _StrudelPassword);
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

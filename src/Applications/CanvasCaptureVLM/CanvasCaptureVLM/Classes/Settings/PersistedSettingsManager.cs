using CanvasCaptureVLM.Classes.Logging;

namespace CanvasCaptureVLM.Classes.Settings
{
    internal enum SettingsKeys
    {
        ImageDirectory
    }

    internal class PersistedSettingsManager
    {
        private const string imgDirEnvVarKey = "PaintingMusicCanvasCaptureImageDirectory";

        private static readonly Dictionary<SettingsKeys, string> settingToEnvVarKeyMap = new()
        {
            { SettingsKeys.ImageDirectory, imgDirEnvVarKey }
        };

        public static string GetSetting(SettingsKeys settingName)
        {
            if (settingToEnvVarKeyMap.TryGetValue(settingName, out string? envVarKey))
            {
                return Environment.GetEnvironmentVariable(envVarKey) ?? string.Empty;
            }
            
            Log.Warning($"No environment variable mapping found for setting '{settingName}'");
            return string.Empty;
        }
    }
}

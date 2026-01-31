using CanvasCaptureVLM.Classes.Helper;
using System.Security.Cryptography;
using System.Text;

namespace CanvasCaptureVLM.Classes.Settings.SecureSettings
{
    internal static class SecureSettingsHandler
    {
        private readonly static string[] acceptedSecureItems = ["api_key", "strudel_password"];

        public static void SaveSecureSetting(string key, string value)
        {
            if (!acceptedSecureItems.Contains(key))
            {
                throw new ArgumentException("The provided key is not accepted for secure settings.");
            }

            byte[] data = Encoding.UTF8.GetBytes(value);

            byte[] encrypted = ProtectedData.Protect(
                data,
                optionalEntropy: null,
                scope: DataProtectionScope.CurrentUser
            );

            var appdataPath = DirectoryHelper.GetAppDataDirectory();
            var filePath = Path.Combine(appdataPath, key + ".dat");
            File.WriteAllBytes(filePath, encrypted);
        }

        public static string LoadSecureSetting(string key)
        {
            if (!acceptedSecureItems.Contains(key))
            {
                throw new ArgumentException("The provided key is not accepted for secure settings.");
            }

            var appdataPath = DirectoryHelper.GetAppDataDirectory();
            var filePath = Path.Combine(appdataPath, key + ".dat");
            
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }
            byte[] encrypted = File.ReadAllBytes(filePath);
            byte[] decrypted = ProtectedData.Unprotect(
                encrypted,
                optionalEntropy: null,
                scope: DataProtectionScope.CurrentUser
            );
            return Encoding.UTF8.GetString(decrypted);
        }
    }
}

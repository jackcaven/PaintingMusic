namespace CanvasCaptureVLM.Classes.Helper
{
    internal static class DirectoryHelper
    {
        public static string GetAppDataDirectory()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appDirectory = Path.Combine(appData, "PaintingMusic", "CanvasCaptureVLM");
            return appDirectory;
        }
    }
}

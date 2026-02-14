using System.Diagnostics;

namespace CanvasCaptureVLM.Classes.Helper
{
    internal static class StrudelLauncher
    {
        private const string strudelUrl = "https://strudel.thinklet.co.uk/";

        internal static void Launch()
        {
            ProcessStartInfo psInfo = new()
            {
                FileName = strudelUrl,
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }
    }
}

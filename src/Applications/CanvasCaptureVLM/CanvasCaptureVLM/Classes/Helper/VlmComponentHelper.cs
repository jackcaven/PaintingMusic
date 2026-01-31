using CanvasCaptureVLM.Classes.VlmClients.Models;

namespace CanvasCaptureVLM.Classes.Helper
{
    internal class VlmComponentHelper
    {
        internal static event Action<VlmResult>? OnVlmDataReceived;

        public static void UpdateVlmComponents(VlmResult vlmResult)
        {
            OnVlmDataReceived?.Invoke(vlmResult);
        }
    }
}

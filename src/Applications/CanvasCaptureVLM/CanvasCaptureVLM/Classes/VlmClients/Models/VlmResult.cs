namespace CanvasCaptureVLM.Classes.VlmClients.Models
{
    internal sealed class VlmResult
    {
        public required Thoughts Thoughts { get; set; }
        public required TokenUsage Usage { get; set; }
    }
}

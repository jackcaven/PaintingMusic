namespace CanvasCaptureVLM.Classes.VlmClients.Models
{
    internal sealed class TokenUsage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens => PromptTokens + CompletionTokens;
    }
}

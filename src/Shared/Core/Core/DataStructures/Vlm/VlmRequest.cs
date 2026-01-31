namespace Core.DataStructures.Vlm
{
    public sealed class VlmRequest
    {
        public string Model { get; init; } = default!;
        public IReadOnlyList<ChatMessage> Messages { get; init; } = [];
    }
}

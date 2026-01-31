namespace Core.DataStructures.Vlm
{
    public sealed class VlmResponse
    {
        public string Text { get; init; } = "";
        public string Model { get; init; } = "";
        public TimeSpan Duration { get; init; }
    }
}

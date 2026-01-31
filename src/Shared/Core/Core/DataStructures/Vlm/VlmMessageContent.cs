using Core.Interfaces;

namespace Core.DataStructures.Vlm
{
    public sealed class TextContent : IVlmMessageContent
    {
        string Text { get; init; } = "";
    }

    public sealed class ImageContent : IVlmMessageContent
    {
        public string FilePath { get; init; } = "";
        public byte[]? Bytes { get; init; }
        public string? MimeType { get; init; }
    }
}

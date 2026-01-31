using Core.Enums.AI;
using Core.Interfaces;

namespace Core.DataStructures.Vlm
{
    public sealed class ChatMessage
    {
        public VlmChatRole Role { get; init; }
        public IReadOnlyList<IVlmMessageContent> Content { get; init; } = [];
    }
}

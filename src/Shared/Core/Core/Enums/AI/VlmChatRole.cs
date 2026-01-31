using System.Text.Json.Serialization;

namespace Core.Enums.AI
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VlmChatRole
    {
        System,
        User,
        Assistant,
        Tool
    }
}

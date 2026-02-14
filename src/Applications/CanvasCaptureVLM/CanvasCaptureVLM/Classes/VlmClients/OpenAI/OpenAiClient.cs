using CanvasCaptureVLM.Classes.VlmClients.Models;
using OpenAI.Chat;
using System.Text.Json;

namespace CanvasCaptureVLM.Classes.VlmClients.OpenAI
{
    internal class OpenAiClient(string apiKey)
    {
        private readonly ChatClient chatClient = new("gpt-4o", apiKey);


        public async Task<VlmResult> SendPrompt(string primer,
            BinaryData isolatedImage,
            BinaryData canvasImage,
            IEnumerable<string> previousInstructions,
            CancellationToken cancellationToken = default)
        {
            SystemChatMessage systemMessage = new(primer);

            List<ChatMessageContentPart> userContentParts =
            [
                ChatMessageContentPart.CreateTextPart($" Previous music instruction:\n{string.Join("\n", previousInstructions)}"),
                ChatMessageContentPart.CreateImagePart(isolatedImage, "image/jpeg"),
                ChatMessageContentPart.CreateImagePart(canvasImage, "image/jpeg")
            ];
            UserChatMessage userMessage = new(userContentParts);

            ChatMessage[] messages =
            [
                systemMessage,
                userMessage
            ];


            ChatCompletionOptions options = new()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
            };

            ChatCompletion chatCompletion = await chatClient.CompleteChatAsync(messages, options, cancellationToken: cancellationToken);

            string responseJson = chatCompletion.Content[0].Text ?? throw new InvalidOperationException("No content in response message.");

            using var doc = JsonDocument.Parse(responseJson);
            var thoughtsJson = doc.RootElement.GetProperty("Thoughts").GetRawText();

            Thoughts thoughts = System.Text.Json.JsonSerializer.Deserialize<Thoughts>(thoughtsJson)
                ?? throw new InvalidOperationException("Failed to deserialize response JSON to get thoughts.");

            VlmResult result = new()
            {
                Thoughts = thoughts,
                Usage = new()
                {
                    PromptTokens = chatCompletion.Usage.InputTokenDetails.CachedTokenCount,
                    CompletionTokens = chatCompletion.Usage.OutputTokenDetails.ReasoningTokenCount,
                }
            };

            return result;
        }
    }
}

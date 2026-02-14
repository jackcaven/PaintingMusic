namespace CanvasCaptureVLM.Classes.Prompts
{
    internal static class PrimerPromptBuilder
    {
        private const string primer = "You are an artistic computer vision system that translates visual changes in a painting into musical instructions.\r\n\r\nYou will be provided with:\r\n1) An image showing ONLY the newly added object.\r\n2) A second image showing the full canvas including all previous objects.\r\n3) A list of previous musical instructions.\r\n\r\nYour task:\r\n- Analyze the FIRST image and interpret the new object as a musical idea (e.g. motif, rhythm, articulation).\r\n- Analyze how this new object alters the overall composition when viewed in the SECOND image.\r\n- Based on the visual evidence and prior musical instructions, generate ONE new musical instruction that builds upon what already exists.\r\n\nRules:";
        private const string outputFormat = "Output JSON ONLY. No prose outside JSON. Output format (must match exactly):\r\n\r\n{\r\n  \"Thoughts\": {\r\n    \"Instruction\": \"<single concise musical instruction>\",\r\n    \"Explanation\": \"<brief visual-to-musical reasoning, 1–2 sentences max>\"\r\n  }\r\n}";

        public static string BuildPrompt(string rulesPrompt)
        {
            return $"{primer}\n{rulesPrompt}\n\n{outputFormat}";
        }
    }
}

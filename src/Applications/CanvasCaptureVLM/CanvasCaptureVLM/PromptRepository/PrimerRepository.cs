namespace CanvasCaptureVLM.PromptRepository
{
    internal sealed class PrimerRepository
    {
        private readonly string promptsDirectory = Path.Combine(AppContext.BaseDirectory, "PromptRepository");

        internal string LoadPrompt(string promptName)
        {
            string promptPath = Path.Combine(promptsDirectory, promptName + ".txt");
            if (!File.Exists(promptPath))
            {
                throw new FileNotFoundException($"Prompt file '{promptName}.txt' not found in PromptRepository.");
            }
            return File.ReadAllText(promptPath);
        }
    }
}

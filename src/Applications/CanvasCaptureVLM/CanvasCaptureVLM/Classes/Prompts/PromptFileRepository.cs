using CanvasCaptureVLM.Classes.Helper;
using CanvasCaptureVLM.Classes.Logging;
using CanvasCaptureVLM.Interfaces.Repositories;

namespace CanvasCaptureVLM.Classes.Prompts
{
    internal sealed class PromptFileRepository : IPromptRepository
    {
        private static readonly Lazy<PromptFileRepository> instance = new(() => new PromptFileRepository());

        private readonly string promptRepositoryDirectory = string.Empty;

        public static PromptFileRepository Instance => instance.Value;

        public PromptFileRepository()
        {
            promptRepositoryDirectory = Path.Combine(DirectoryHelper.GetAppDataDirectory(), "Prompts");
            SetUpRepository();
        }

        public async Task Add(string name, string prompt)
        {
            string filePath = Path.Combine(promptRepositoryDirectory, name);

            if (File.Exists(filePath))
            {
                Log.Warning($"Overwritting prompt file {name}", nameof(PromptFileRepository));
                File.Delete(filePath);
            }

            using StreamWriter streamWriter = new(filePath);
            streamWriter.WriteLine(prompt);
        }

        public async Task Update(string name, string prompt)
        {
            string filePath = Path.Combine(promptRepositoryDirectory, name);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Could not find file {name}", nameof(PromptFileRepository));

            await File.WriteAllTextAsync(filePath, prompt);
        }

        public IEnumerable<string> GetPromptNames()
        {
            try
            {
                return Directory.EnumerateFiles(promptRepositoryDirectory).Select(x => Path.GetFileNameWithoutExtension(x));
            }
            catch
            {
                Log.Warning("No prompt files in repo directory", nameof(PromptFileRepository));
                return [];
            }
        }


        public async Task<string> GetPrompt(string name)
        {
            string filePath = Path.Combine(promptRepositoryDirectory, name);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{name} does not exist in prompt repository");
            }

            string results = await File.ReadAllTextAsync(filePath);

            return results;
        }

        public async Task Remove(string name)
        {
            string filePath = Path.Combine(promptRepositoryDirectory, name);

            if (File.Exists(filePath))
            {
                Log.Debug($"Deleting {name} from prompt repository", nameof(PromptFileRepository));
                File.Delete(filePath);
            }
        }

        private void SetUpRepository()
        {
            Directory.CreateDirectory(promptRepositoryDirectory);

            string defaultPromptPath = Path.Combine(AppContext.BaseDirectory, "Resources", "Prompts", "Default.txt");
            string targetPromptPath = Path.Combine(promptRepositoryDirectory, "Default.txt");

            if (!File.Exists(defaultPromptPath))
            {
                throw new FileNotFoundException($"Default prompt not found during set up");
            }

            if (!File.Exists(targetPromptPath))
            {
                File.Move(defaultPromptPath, targetPromptPath);
            }
        }
    }
}

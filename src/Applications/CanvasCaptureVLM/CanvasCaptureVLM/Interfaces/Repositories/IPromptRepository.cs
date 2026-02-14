using CanvasCaptureVLM.Classes.Prompts;
using System.Collections.Generic;

namespace CanvasCaptureVLM.Interfaces.Repositories
{
    public interface IPromptRepository
    {
        Task Add(string name, string prompt);

        Task Update(string name, string prompt);

        Task Remove(string name);

        IEnumerable<string> GetPromptNames();

        Task<string> GetPrompt(string name);
    }
}

namespace CanvasCapture.Classes
{
    public class ModelFeedbackFileWriter(string directoryPath)
    {
        private const string FileName = "ModelFeedback.txt";
        private readonly string FilePath = Path.Join(directoryPath, FileName);

        public async Task Write(string content)
        {
            if (!File.Exists(FilePath))
            {
                return;
            }

            await File.AppendAllTextAsync(FilePath, content + Environment.NewLine);
        }
    }
}

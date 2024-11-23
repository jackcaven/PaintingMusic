using System.Reflection;

namespace MusicGeneration.Models.Markov.Helpers
{
    internal static class CsvLoader
    {
        public static List<string[]> LoadCsv(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new FileNotFoundException("Resource not found.");
            using var reader = new StreamReader(stream);
            var rows = new List<string[]>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                    rows.Add(line.Split(',')); // Split values by commas
            }
            return rows;
        }
    }
}

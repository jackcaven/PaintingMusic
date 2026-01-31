namespace CanvasCaptureVLM.Classes.Performance.ImageHandling
{
    internal static class ImageDirectoryOrganiser
    {
        internal static void OrganiseDirectory(string targetDirectory)
        {
            if (!Directory.Exists(targetDirectory))
            {
                Console.WriteLine("The specified directory does not exist.");
                return;
            }

            string subDirectory = Path.Combine(targetDirectory, Guid.NewGuid().ToString());
            Directory.CreateDirectory(subDirectory);


            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
            var imageFiles = Directory.GetFiles(targetDirectory)
                                        .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                                        .ToList();

            foreach (var image in imageFiles)
            {
                string fileName = Path.GetFileName(image);
                string destinationPath = Path.Combine(subDirectory, fileName);
                File.Move(image, destinationPath);
            }
        }
    }
}

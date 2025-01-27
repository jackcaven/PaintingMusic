using CanvasCapture.Classes;
using CanvasCapture.Interfaces;
using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.Enums.AI;
using Core.Interfaces;
using MusicGeneration;
using Serilog;
namespace CanvasCaptureUI.Classes
{
    internal class Performance(PictureBox displayBox) : ICanvasCaptureProcess
    {
        private readonly string imageDirectory = AppSettingsManager.GetImageDirectory();
        private readonly ICoreMusicProducer coreMusicProducer = CoreMusicGeneratorFactory.ConstructMusicGenerator(Model.Markov);
        private readonly ImageCropper cropper = new(displayBox);
        private readonly List<ObjectAttributes> objectAttributesCache = [];
        private CanvasAttributes? canvasAttributesCache;
        private FileSystemWatcher? fileSystemWatcher;
        private Image? imageCache;
        private Rectangle? canvasCache = null;
        private bool isRunning = false;

        public bool IsRunning => isRunning;

        Task ICanvasCaptureProcess.Start()
        {
            Log.Information("Starting performance");
            Log.Debug($"File watcher watching: {imageDirectory}");

            fileSystemWatcher = new()
            {
                Path = imageDirectory,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                Filter = "*.*"
            };

            fileSystemWatcher.Created += OnImageReceived;
            fileSystemWatcher.EnableRaisingEvents = true;

            isRunning = true;

            return Task.CompletedTask;
        }

        Task ICanvasCaptureProcess.Stop()
        {
            Log.Warning("Terminating perfromance session");
            fileSystemWatcher!.EnableRaisingEvents = false;
            fileSystemWatcher?.Dispose();
            isRunning = false;
            canvasCache = null;
            imageCache = null;
            coreMusicProducer.Clear();
            MessageBox.Show("Performance has now ended", "Performance Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return Task.CompletedTask;
        }

        private async void OnImageReceived(object sender, FileSystemEventArgs e)
        {
            try
            {
                while (!IsFileReady(e.FullPath))
                {
                    await Task.Delay(500);
                }

                Image image = new Bitmap(e.FullPath);

                image = new Bitmap(image, new((int)image.Width / 2, (int)image.Height / 2));

                // Initial Canvas Crop
                if (canvasCache is null)
                {
                    displayBox.Image = image;
                    canvasCache = await cropper.GetCanvasArea();
                    displayBox.Image = null;
                    return;
                }

                // Initialize image for analysis
                Image postProcessingImage;

                // Crop to canvas
                Image croppedImage = image.CropToCanvas((Rectangle)canvasCache);
                
                // Handle first non-canvas image
                if (imageCache is null)
                {
                    postProcessingImage = croppedImage;
                }
                else
                {
                    postProcessingImage = croppedImage.SubtractImages(imageCache);
                }

                // Update image cache
                imageCache = croppedImage;

                // Crop image to painted object
                Image objectImage = postProcessingImage.CropToObject();

                // Get data from image and send to PM
                ObjectAttributes objectAttributes = objectImage.GetObjectAttributes(imageCache);
                objectAttributesCache.Add(objectAttributes);

                canvasAttributesCache = new()
                {
                    AreaCovered = objectAttributesCache.Sum(x => x.Area) >= 1 ? 1 : objectAttributesCache.Sum(x => x.Area),
                    COG = (objectAttributesCache.Select(o => o.COG.X).Average(), objectAttributesCache.Select(o => o.COG.Y).Average())
                };


                MusicData musicData = coreMusicProducer.Add(objectAttributes, canvasAttributesCache);

                Log.Information("Sending payload to music player...");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static bool IsFileReady(string filePath)
        {
            try
            {
                using FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                return stream.Length > 0;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}

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
    internal class Performance(PictureBox displayBox, ComboBox InstrumentSelection) : ICanvasCaptureProcess
    {
        private readonly string imageDirectory = AppSettingsManager.GetImageDirectory();
        private readonly ICoreMusicProducer coreMusicProducer = CoreMusicGeneratorFactory.ConstructMusicGenerator(Model.Markov);
        private readonly ImageCropper cropper = new(displayBox);
        private readonly MusicPlayerClient playerClient = new();
        private readonly List<ObjectAttributes> objectAttributesCache = [];
        
        private CanvasAttributes? canvasAttributesCache;
        private FileSystemWatcher? fileSystemWatcher;
        private Image? imageCache;
        private Rectangle? canvasCache = null;
        private bool isRunning = false;

        public bool IsRunning => isRunning;

        async Task ICanvasCaptureProcess.Start()
        {
            Log.Information("Starting performance");
            Log.Debug($"File watcher watching: {imageDirectory}");

            fileSystemWatcher = new()
            {
                Path = imageDirectory,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                Filter = "*.*"
            };

            fileSystemWatcher.Created += async (s, e) => await OnImageReceived(e.FullPath);
            fileSystemWatcher.EnableRaisingEvents = true;

            await playerClient.Start();
            isRunning = true;
        }

        async Task ICanvasCaptureProcess.Stop()
        {
            Log.Warning("Stopping performance session...");

            if (fileSystemWatcher != null)
            {
                fileSystemWatcher.EnableRaisingEvents = false;
                fileSystemWatcher.Dispose();
                fileSystemWatcher = null;
            }

            isRunning = false;
            canvasCache = null;
            imageCache = null;
            objectAttributesCache.Clear();

            coreMusicProducer.Clear();
            await playerClient.Stop();
            MessageBox.Show("Performance has now ended", "Performance Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task OnImageReceived(string filePath)
        {
            try
            {
                while (!IsFileReady(filePath))
                {
                    await Task.Delay(500);
                }

                using var image = new Bitmap(filePath);

                var resizedImage = new Bitmap(image, new((int)image.Width / 2, (int)image.Height / 2));

                // Initial Canvas Crop
                if (canvasCache is null)
                {
                    displayBox.Image = resizedImage;
                    canvasCache = await cropper.GetCanvasArea();
                    displayBox.Image = null;
                    return;
                }

                // Crop to canvas
                Image croppedImage = resizedImage.CropToCanvas(canvasCache.Value);
                
                // Process image difference
                var postProcessingImage = imageCache == null ? croppedImage : croppedImage.SubtractImages(imageCache);
                imageCache = croppedImage;

                // Crop image to painted object
                Image objectImage = postProcessingImage.CropToObject();

                // Get data from image and send to PM
                ObjectAttributes objectAttributes = objectImage.GetObjectAttributes(imageCache);
                Log.Debug("Object Attribute Data: {ObjectData}", objectAttributes);
                objectAttributesCache.Add(objectAttributes);

                canvasAttributesCache = new()
                {
                    AreaCovered = Math.Min(1, objectAttributesCache.Sum(o => o.Area)),
                    COG = (
                        objectAttributesCache.Average(o => o.COG.X),
                        objectAttributesCache.Average(o => o.COG.Y)
                    )
                };

                Log.Debug("Canvas Attributes: {CanvasData}", canvasAttributesCache);

                MusicData musicData = coreMusicProducer.Add(objectAttributes, canvasAttributesCache);

                musicData.Instrument = InstrumentSelection.SelectedText;

                Log.Debug("Music Data: {Music Data}", musicData);

                await playerClient.SendPayload(musicData);

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

using CanvasCaptureUI.Classes;
using CanvasCapture.Classes;
using Core.DataStructures.Art;
using Core.Interfaces;
using Core.Enums.AI;
using MusicGeneration;
using Core.DataStructures.Music;
using CanvasCapture.Interfaces;
using Serilog;
using Core.DataStructures.Result;

namespace CanvasCaptureUI.Simulation
{
    internal class ImageProcessingSimulator(PictureBox displayBox) : ICanvasCaptureProcess
    {
        private readonly string imageDirectory = AppSettingsManager.GetImageDirectory("Simulation");
        private readonly PictureBox DisplayBox = displayBox;
        private readonly ImageCropper ImageCropper = new(displayBox);
        private bool isRunning = false;
        private Rectangle? canvasCache = null;
        private Image? ImageCache;
        private CanvasAttributes? CanvasAttributesCache;
        private List<ObjectAttributes> ObjectAttributesCache = [];
        private readonly ICoreMusicProducer coreMusicProducer = CoreMusicGeneratorFactory.ConstructMusicGenerator(Model.Markov);

        public bool IsRunning => isRunning;

        public string[] Instruments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task Start()
        {
            try
            {
                if (isRunning)
                {
                    MessageBox.Show("Simulation already running", "Be patient", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }


                isRunning = true;

                // Get all image files from the directory
                var imageFiles = Directory.GetFiles(imageDirectory, "*.*")
                    .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                                   file.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (imageFiles.Count == 0)
                {
                    MessageBox.Show("No images found in the directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int imgCount = 0;

                foreach (var imageFile in imageFiles)
                {
                    if (!IsRunning)
                    {
                        Log.Information("Stopping simulation");
                        break;
                    }

                    Log.Debug("Loading image...");
                    Bitmap image = new(imageFile);
                    Log.Debug("Original Image size: {0} x {1}", image.Width, image.Height);

                    ImagePreparationHelpers.ReduceImageSize(ref image);
                    Log.Debug("Reduced Image size: {0} x {1}", image.Width, image.Height);
                    DisplayBox.Image = image;
                    
                    await Task.Delay(500);

                    canvasCache ??= await ImageCropper.GetCanvasArea();
                    ImageCropper.Dispose();

                    ImagePreparationHelpers.CropToCanvas(ref image, (Rectangle)canvasCache);
                    DisplayBox.Image = image;
                    Log.Debug("Crop to Canvas Image size: {0} x {1}", image.Width, image.Height);

                    if (imgCount == 0)
                    {
                        // Return as we do not want to process canvas image
                        imgCount++;
                        ImageCache = image;
                        continue;
                    }

                    Bitmap diffImage = (Bitmap)image.SubtractImages(ImageCache!);

                    DisplayBox.Image = diffImage;
                    await Task.Delay(200);
                    DisplayBox.Image = null;
                    ImagePreparationHelpers.CropToObject(ref diffImage, out Point objectCanvasLocation);
                    DisplayBox.Image = diffImage;
                    Log.Debug("Object Size: {0} x {1}", diffImage.Width, diffImage.Height);
                    await Task.Delay(200);
                    ObjectAttributes data = ImageDataMinerExtensions.GetObjectAttributes(ref diffImage, image, objectCanvasLocation, Log.Logger);
                    Log.Information($"{data}{Environment.NewLine}{Environment.NewLine}");

                    ObjectAttributesCache.Add(data);
                    CanvasAttributesCache = new()
                    {
                        AreaCovered = ObjectAttributesCache.Sum(x => x.Area) >= 1 ? 1 : ObjectAttributesCache.Sum(x => x.Area),
                        COG = (ObjectAttributesCache.Select(o => o.COG.X).Average(), ObjectAttributesCache.Select(o => o.COG.Y).Average())
                    };

                    Log.Information($"{CanvasAttributesCache}{Environment.NewLine}{Environment.NewLine}");

                    CoreResult result = coreMusicProducer.Add(data, CanvasAttributesCache);

                    Log.Information($"{result.ModelDecisionLogic}");
                    Log.Information($"{result.MusicData}{Environment.NewLine}{Environment.NewLine}");

                    ImageCache = image;
                }

                Log.Information("Simulation complete!");
                MessageBox.Show("Simulation Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            finally
            {
                isRunning = false;        
            }
        }

        public Task Stop()
        {
            isRunning = false;
            CanvasAttributesCache = null;
            ObjectAttributesCache.Clear();
            CanvasAttributesCache = null;

            return Task.CompletedTask;
        }
    }
}

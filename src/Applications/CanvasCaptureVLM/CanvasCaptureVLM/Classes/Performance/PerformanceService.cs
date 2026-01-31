using CanvasCaptureVLM.Classes.Helper;
using CanvasCaptureVLM.Classes.Logging;
using CanvasCaptureVLM.Classes.MusicClient.Strudel;
using CanvasCaptureVLM.Classes.Performance.ImageHandling;
using CanvasCaptureVLM.Classes.Settings;
using CanvasCaptureVLM.Classes.VlmClients.Models;
using CanvasCaptureVLM.Classes.VlmClients.OpenAI;
using CanvasCaptureVLM.PromptRepository;
using System.Drawing.Imaging;

namespace CanvasCaptureVLM.Classes.Performance
{
    internal class PerformanceService(SettingsService settingsService, PictureBox canvasBox)
    {
        private readonly PictureBox canvasBox = canvasBox;
        private readonly SettingsService settingsService = settingsService;
        private readonly ImageCropper imageCropper = new(canvasBox);
        private readonly PrimerRepository primerRepository = new();
        private readonly OpenAiClient openAiClient = new(settingsService.Settings.APIKey);
        private readonly StrudelClient strudelClient = new();

        private ImageWatcherServices? imgDirWatcher;
        private Rectangle? canvasCache;
        private Image? imageCache;
        private IEnumerable<string> VlmResponses = [];

        internal async Task<bool> Login()
        {
            try
            {
                return await strudelClient.LoginAsync(settingsService.Settings.StrudelEmail, settingsService.Settings.StrudelPassword);
            }
            catch (Exception ex)
            {
                Log.Error($"Error trying to log into Strudel: {ex}", nameof(PerformanceService));
                return false;
            }
        }
        
        internal void Start()
        {
            Log.Info("Performance process started.");
            Log.Debug($"File watcher watching {settingsService.Settings.ImageDirectory}");

            if (string.IsNullOrEmpty(settingsService.Settings.APIKey))
            {
                Log.Warning("No API Key set. Cannot send requests to VLM.");
            }

            if (!strudelClient.IsLoggedIn)
            {
                Log.Warning("User not logged into Strudel.  No music will be played");
            }

            imgDirWatcher = new ImageWatcherServices(settingsService.Settings.ImageDirectory);
            imgDirWatcher.ImageReady += async (sender, fullPath) => await OnImageReceived(fullPath);
        }        

        internal async void Stop()
        {
            Log.Info("PerformanceService stopped.");

            imgDirWatcher?.Dispose();

            canvasCache = null;
            imageCache = null;

            canvasBox?.Image?.Dispose();
            ImageDirectoryOrganiser.OrganiseDirectory(settingsService.Settings.ImageDirectory);

            VlmResponses = [];

            await strudelClient.Reset();

            Log.Info("Performance shutdown complete");
        }

        private async Task OnImageReceived(object fullPath)
        {
            Log.Debug($"New image detected at path: {fullPath}");

            try
            {
                var image = new Bitmap(fullPath.ToString()!);
                Log.Debug($"Original image siez: {image.Width} x {image.Height}");

                ImageHelper.ReduceImageSize(ref image);
                Log.Debug($"Reduced image size: {image.Width} x {image.Height}");

                // Initial canvas crop
                if (canvasCache == null)
                {
                    canvasBox.Image = image;
                    canvasCache = await imageCropper.GetCanvasArea();
                    Log.Debug($"Canvas area saved is {canvasCache?.Width} x {canvasCache?.Height} = {canvasCache?.Width * canvasCache?.Height}. Disposing cropper instance.");
                    imageCropper.Dispose();
                    ImageHelper.CropToCanvas(ref image, canvasCache!.Value);
                    imageCache = image;
                    return;
                }

                // Crop to canvas
                ImageHelper.CropToCanvas(ref image, canvasCache.Value);

                // Image subtraction (if required)
                Bitmap postProcessingImage = imageCache == null ? (Bitmap)image : (Bitmap)image.SubtractImages(imageCache);
                imageCache = image;
                Log.Debug("Image subtraction successful");

                // Build Prompt
                string prompt = primerRepository.LoadPrompt("primer");

                // Send to VLM with images
                BinaryData img1 = ImageHelper.ImageToBinaryData(postProcessingImage, ImageFormat.Jpeg);
                BinaryData img2 = ImageHelper.ImageToBinaryData(image, ImageFormat.Jpeg);

                canvasBox.Image = postProcessingImage;

                if (string.IsNullOrEmpty(settingsService.Settings.APIKey))
                {
                    Log.Warning("No API Key.  Skipping sending prompt to VLM");
                    return;
                }
                
                Log.Debug("Sending reposnse to VLM");

                VlmResult vlmResult = await openAiClient.SendPrompt(prompt, img1, img2, VlmResponses);

                VlmComponentHelper.UpdateVlmComponents(vlmResult);

                VlmResponses = VlmResponses.Append(vlmResult.Thoughts.Instruction);

                if (strudelClient.IsLoggedIn)
                {
                    Log.Debug("Sending instruction to Strudel");
                    await strudelClient.Prompt(vlmResult.Thoughts.Instruction);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error handling image at path {fullPath}: {ex.Message}", nameof(PerformanceService));
            }
        }
    }
}

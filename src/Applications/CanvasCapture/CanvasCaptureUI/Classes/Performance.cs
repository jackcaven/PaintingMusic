using CanvasCapture.Classes;
using CanvasCapture.Interfaces;
using Core.DataStructures.Art;
using Core.DataStructures.Music;
using Core.DataStructures.Result;
using Core.Enums.AI;
using Core.Interfaces;
using MusicGeneration;
using Serilog;
namespace CanvasCaptureUI.Classes
{
    internal class Performance(PictureBox displayBox, bool logModelDecisions) : ICanvasCaptureProcess
    {
        private const string fileWriterEndMessage = "|END|";

        private readonly string imageDirectory = AppSettingsManager.GetImageDirectory("Performance");
        private readonly ICoreMusicProducer coreMusicProducer = CoreMusicGeneratorFactory.ConstructMusicGenerator(Model.Markov);
        private readonly ImageCropper cropper = new(displayBox);
        private readonly MusicPlayerClient playerClient = new();
        private readonly List<ObjectAttributes> objectAttributesCache = [];
        private readonly ModelFeedbackFileWriter? modelFeedbackWriter = logModelDecisions ? new ModelFeedbackFileWriter(AppSettingsManager.GetImageDirectory("Performance")) : null;
        
        private CanvasAttributes? canvasAttributesCache;
        private FileSystemWatcher? fileSystemWatcher;
        private Image? imageCache;
        private Rectangle? canvasCache = null;
        private bool isRunning = false;

        public bool IsRunning => isRunning;

        public string[] Instruments { get; set; } = ["piano"];

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
            modelFeedbackWriter?.Write(fileWriterEndMessage + Environment.NewLine);
            await playerClient.Stop();
            fileSystemWatcher?.Dispose();
            displayBox?.Image?.Dispose();
            ImageOrganiser.OrganiseDirectory(imageDirectory);
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

                var image = new Bitmap(filePath);
                Log.Debug("Original Image size: {0} x {1}", image.Width, image.Height);

                ImagePreparationHelpers.ReduceImageSize(ref image);
                Log.Debug("Reduced Image size: {0} x {1}", image.Width, image.Height);

                // Initial Canvas Crop
                if (canvasCache is null)
                {
                    displayBox.Image = image;
                    canvasCache = await cropper.GetCanvasArea();
                    Log.Information("Canvas Area: {0} x {1}", canvasCache?.Width, canvasCache?.Height);
                    cropper.Dispose();
                    ImagePreparationHelpers.CropToCanvas(ref image, canvasCache!.Value);
                    imageCache = image;
                    return;
                }

                // Crop to canvas
                ImagePreparationHelpers.CropToCanvas(ref image, canvasCache.Value);
                Log.Debug("Canvas Cropped Image size: {0} x {1}", image.Width, image.Height);

                // Process image difference
                Bitmap postProcessingImage = imageCache == null ? (Bitmap)image : (Bitmap)image.SubtractImages(imageCache);
                imageCache = image;
                Log.Debug("Image subtraction successful");

                // Crop image to painted object
                ImagePreparationHelpers.CropToObject(ref postProcessingImage, out Point objectCanvasLocation);
                Log.Debug("Object Image size: {0} x {1}", postProcessingImage.Width, postProcessingImage.Height);

                // Get data from image and send to PM
                ObjectAttributes objectAttributes = ImageDataMinerExtensions.GetObjectAttributes(ref postProcessingImage, imageCache, objectCanvasLocation, Log.Logger);
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

                CoreResult result = coreMusicProducer.Add(objectAttributes, canvasAttributesCache);

                result.MusicData.Instrument = Instruments[0];

                MusicData musicData = result.MusicData;

                List<Note> adjustedNotes = AdjustPitches(ref musicData);

                if (adjustedNotes.Count != 0)
                {
                    musicData = new()
                    {
                        Instrument = musicData.Instrument,
                        BPM = musicData.BPM,
                        Notes = adjustedNotes
                    };
                }

                displayBox.Image = postProcessingImage;

                Log.Debug($"Music Data: {musicData}");

                modelFeedbackWriter?.Write(result.ModelDecisionLogic);

                await playerClient.SendPayload(musicData);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                Log.Error(ex, "Message: {0}\n Stack Trace: {1}", ex.Message, ex.StackTrace);
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

        private static List<Note> AdjustPitches(ref MusicData coreMusic)
        {
            if (coreMusic.Instrument != "Violin")
                return [];

            List<Note> notes = [];
            
            foreach (Note note in coreMusic.Notes)
            {
                List<int> pitches = [];

                for (int i = 0; i < note.Notes.Count(); i++)
                {
                    int pitch = note.Notes.ToArray()[i];

                    if (pitch == 50)
                        pitch += 5;
                    else if (pitch < 50)
                        pitch += 50 - pitch;

                    if (pitch == 50)
                            pitch += 5;

                    pitches.Add(pitch);
                }

                notes.Add(new()
                {
                    Notes = pitches,
                    StartTime = note.StartTime,
                    Duration = note.Duration,
                    Velocity = note.Velocity,
                });
            }

            return notes;
        }

    }
}

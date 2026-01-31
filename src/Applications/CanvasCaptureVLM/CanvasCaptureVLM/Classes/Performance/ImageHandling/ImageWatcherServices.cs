using CanvasCaptureVLM.Classes.Logging;

namespace CanvasCaptureVLM.Classes.Performance.ImageHandling
{
    internal class ImageWatcherServices : IDisposable
    {
        private const int maxAttempts = 20;
        private const int delayBetweenAttemptsMs = 50;

        private readonly FileSystemWatcher watcher;

        internal event EventHandler<string>? ImageReady;


        internal ImageWatcherServices(string directoryToWatch)
        {
            watcher = new FileSystemWatcher
            {
                Path = directoryToWatch,
                Filter = "*.*",
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };
            watcher.Created += OnCreated;
        }

        public void Dispose()
        {
            watcher.Created -= OnCreated;
            watcher.Dispose();
        }

        private async void OnCreated(object sender, FileSystemEventArgs e)
        {
            Log.Debug($"File created event detected for file: {e.FullPath}"); 
            // Fire-and-forget async event handler is intentional here
            for (int i = 0; i < maxAttempts; i++)
            {
                if (await IsFileReadyAsync(e.FullPath))
                {
                    ImageReady?.Invoke(this, e.FullPath);
                    return;
                }

                await Task.Delay(delayBetweenAttemptsMs);
            }
        }

        private static async Task<bool> IsFileReadyAsync(string path)
        {
            try
            {
                long size1, size2;

                using (var stream = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None))
                {
                    size1 = stream.Length;
                }

                await Task.Delay(delayBetweenAttemptsMs);

                using (var stream = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None))
                {
                    size2 = stream.Length;
                }

                return size1 > 0 && size1 == size2;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}

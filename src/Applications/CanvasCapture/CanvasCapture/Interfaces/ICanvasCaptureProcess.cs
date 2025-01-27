namespace CanvasCapture.Interfaces
{
    public interface ICanvasCaptureProcess
    {
        bool IsRunning { get; }

        Task Start();

        Task Stop();
    }
}

namespace CanvasCapture.Interfaces
{
    public interface ICanvasCaptureProcess
    {
        bool IsRunning { get; }

        string[] Instruments { get; set; }

        Task Start();

        Task Stop();
    }
}

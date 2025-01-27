using CanvasCapture.Classes;
using CanvasCapture.Interfaces;
using CanvasCaptureUI.Classes;
using CanvasCaptureUI.Simulation;
using Serilog;

namespace CanvasCapture
{
    public partial class CanvasCaptureMain : Form
    {
        private ICanvasCaptureProcess canvasCaptureProcess;
        private readonly string imageDirectory = AppSettingsManager.GetImageDirectory();
        private readonly Dictionary<string, bool> options;
        private readonly Dictionary<string, bool> defaultOptions = new()
        {
            { "Enable image viewer", true },
            { "Enable logging of image data", true },
            { "Run as a simulation", false },
        };

        public CanvasCaptureMain()
        {
            InitializeComponent();
            options = defaultOptions;
            canvasCaptureProcess = new Performance(pictureBoxImages);
            ConfigureSettings();
            ConfigureLogger();
        }

        #region Button Controls
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (canvasCaptureProcess.IsRunning)
            {
                MessageBox.Show("Canvas Capture Process is running, please stop first before starting again.", "Canvas Capture is running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (IsRunSimulationSelected())
            {
                canvasCaptureProcess = new ImageProcessingSimulator(pictureBoxImages);
            }

            await canvasCaptureProcess.Start();
        }
        private async void buttonStop_Click(object sender, EventArgs e)
        {
            if (canvasCaptureProcess != null && canvasCaptureProcess.IsRunning)
            {
                await canvasCaptureProcess.Stop();
            }
        }
        #endregion


        #region Dev Options
        private void ConfigureSettings()
        {
            checkedListBoxOptions.Items.Clear();

            foreach (var option in defaultOptions.Select((value, i) => new { i, value }))
            {
                checkedListBoxOptions.Items.Add(option.value.Key);
                checkedListBoxOptions.SetItemChecked(option.i, option.value.Value);
            }
        }

        private void checkedListBoxOptions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                options[options.Keys.ToArray()[e.Index]] = true;
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                options[options.Keys.ToArray()[e.Index]] = false;
            }
        }

        private bool IsRunSimulationSelected()
        {
            return checkedListBoxOptions.CheckedItems.Contains(defaultOptions.Keys.Last());
        }
        #endregion

        #region Configuration
        private void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.RichTextBox(
                    richTextBoxControl: richTextBoxDataViewer,
                    minimumLogEventLevel: Serilog.Events.LogEventLevel.Verbose,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception},",
                    autoScroll: true
                )
                .CreateLogger();
        }
        #endregion
    }
}

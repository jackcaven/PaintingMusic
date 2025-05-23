using CanvasCapture.Interfaces;
using CanvasCaptureUI.Classes;
using CanvasCaptureUI.Simulation;
using Serilog;

namespace CanvasCapture
{
    public partial class CanvasCaptureMain : Form
    {
        private const string autoAppShutDownKey = "Shut down on performance end";
        private const string runAsSimKey = "Run as a simulation";
        private const string logModelDecisionsKey = "View model decision feedback";

        private ICanvasCaptureProcess canvasCaptureProcess;
        private readonly Dictionary<string, bool> options;
        private readonly Dictionary<string, bool> defaultOptions = new()
        {
            { autoAppShutDownKey, true },
            { runAsSimKey, false },
            { logModelDecisionsKey, true },
        };
        private readonly List<string> instruments = [
            "Piano",
            "Violin",
            "Choir",
            "Synth"
        ];

        public CanvasCaptureMain()
        {
            InitializeComponent();
            options = defaultOptions;
            ConfigureSettings();
            ConfigureLogger();
            canvasCaptureProcess = new Performance(pictureBoxImages, IsDevOptionSelected(logModelDecisionsKey));
        }

        #region Button Controls
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            if (canvasCaptureProcess.IsRunning)
            {
                MessageBox.Show("Canvas Capture Process is running, please stop first before starting again.", "Canvas Capture is running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (IsDevOptionSelected(runAsSimKey))
            {
                canvasCaptureProcess = new ImageProcessingSimulator(pictureBoxImages);
            }

            comboBoxInstruments.Enabled = false;

            await canvasCaptureProcess.Start();
        }
        private async void buttonStop_Click(object sender, EventArgs e)
        {
            if (canvasCaptureProcess != null && canvasCaptureProcess.IsRunning)
            {
                await canvasCaptureProcess.Stop();
            }

            comboBoxInstruments.Enabled = true;

            if (IsDevOptionSelected(autoAppShutDownKey))
                Application.Exit();
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

            comboBoxInstruments.Items.Clear();

            foreach (var instrument in instruments)
            {
                comboBoxInstruments.Items.Add(instrument);
            }

            comboBoxInstruments.SelectedIndex = 0;
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

        private bool IsDevOptionSelected(string key)
        {
            return checkedListBoxOptions.CheckedItems.Contains(key);
        }

        private void comboBoxInstruments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (canvasCaptureProcess is not null && comboBoxInstruments.SelectedItem is not null)
            {
                canvasCaptureProcess.Instruments = [comboBoxInstruments.SelectedItem?.ToString()];
            }
        }

        #endregion

        #region Configuration
        private void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.RichTextBox(
                    richTextBoxControl: richTextBoxDataViewer,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception},",
                    autoScroll: true
                )
                .MinimumLevel.Verbose()
                .CreateLogger();
        }
        #endregion

    }
}

using CanvasCaptureVLM.Classes.Helper;
using CanvasCaptureVLM.Classes.Logging;
using CanvasCaptureVLM.Classes.Performance;
using CanvasCaptureVLM.Classes.Prompts;
using CanvasCaptureVLM.Classes.Settings;
using CanvasCaptureVLM.Classes.VlmClients.Models;
using CanvasCaptureVLM.Interfaces.Repositories;
using System.ComponentModel;
using System.Diagnostics;

namespace CanvasCaptureVLM.Pages
{
    public partial class Performance : Form
    {
        private const int MaxLogLines = 50;

        private readonly SettingsService SettingsService;
        private readonly PerformanceService PerformanceService;
        private readonly IPromptRepository promptRepository = PromptFileRepository.Instance;

        private readonly Stopwatch SceneStopWatch = new();

        internal Performance(SettingsService settingsService)
        {
            InitializeComponent();
            SettingsService = settingsService;
            SettingsService.Settings.PropertyChanged += Settings_PropertyChanged;
            PerformanceService = new(promptRepository, settingsService, pictureBoxCanvas);
            PageSetup();

        }

        private void PageSetup()
        {
            textBoxDevLogs.Visible = SettingsService.Settings.ShowLogs;
            LogManager.OnLogReceived += OnLogReceived;
            VlmComponentHelper.OnVlmDataReceived += OnVlmDataUpdate;
            buttonStop.Enabled = false;

            List<string> prompts = [.. promptRepository.GetPromptNames()];

            foreach (string prompt in prompts)
            {
                comboBoxRuleSelect.Items.Add(prompt);
            }
        }

        #region Logging
        private void OnLogReceived(LogEntry entry)
        {
            if (!SettingsService.Settings.ShowLogs)
                return;

            if (textBoxDevLogs.InvokeRequired)
            {
                textBoxDevLogs.BeginInvoke(new Action(() => AppendLog(entry)));
            }
            else
            {
                AppendLog(entry);
            }

            TrimLogs();
        }

        private void AppendLog(LogEntry log)
        {
            textBoxDevLogs.AppendText(log.ToString() + Environment.NewLine);
        }

        private void TrimLogs()
        {
            var lines = textBoxDevLogs.Lines;
            if (lines.Length > MaxLogLines)
            {
                var trimmedLines = lines.Skip(lines.Length - 1000).ToArray();
                textBoxDevLogs.Lines = trimmedLines;
            }
        }
        #endregion

        #region Settings
        private void Settings_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingsService.Settings.ShowLogs))
            {
                textBoxDevLogs.Visible = SettingsService.Settings.ShowLogs;
            }
        }
        #endregion

        #region Performance Controls
        private void buttonStart_Click(object sender, EventArgs e)
        {
            PerformanceService.Start();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            comboBoxRuleSelect.Enabled = false;
            SceneStopWatch.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            PerformanceService.Stop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            comboBoxRuleSelect.Enabled = true;
            SceneStopWatch.Stop();
            Log.Debug($"Performance Duration: {SceneStopWatch.Elapsed}");
            SceneStopWatch.Reset();
        }
        #endregion

        #region VlmComponents
        private void OnVlmDataUpdate(VlmResult result)
        {

            if (richTextBoxAIThoughts.InvokeRequired)
            {
                richTextBoxAIThoughts.BeginInvoke(new Action(() => UpdateAiThoughts(result.Thoughts)));
            }
            else
            {
                UpdateAiThoughts(result.Thoughts);
            }
        }

        private void UpdateAiThoughts(Thoughts thoughts)
        {
            Font consoleFont = new("Consolas", 10, FontStyle.Regular);
            Font headerFont = new("Consolas", 10, FontStyle.Bold);
            richTextBoxAIThoughts.BackColor = Color.Black;

            // Instruction Header
            richTextBoxAIThoughts.SelectionStart = richTextBoxAIThoughts.TextLength;
            richTextBoxAIThoughts.SelectionColor = Color.LimeGreen;
            richTextBoxAIThoughts.SelectionFont = headerFont;
            richTextBoxAIThoughts.AppendText($"[{DateTime.Now:HH:mm:ss}] Painting Music VLM > ");

            // Instruction Text
            richTextBoxAIThoughts.SelectionColor = Color.White;
            richTextBoxAIThoughts.SelectionFont = consoleFont;
            richTextBoxAIThoughts.AppendText($"{thoughts.Instruction} ");

            // Reasoning Header
            richTextBoxAIThoughts.SelectionStart = richTextBoxAIThoughts.TextLength;
            richTextBoxAIThoughts.SelectionIndent = 25;
            richTextBoxAIThoughts.SelectionColor = Color.FromArgb(0, 192, 192); // Cyan/Teal
            richTextBoxAIThoughts.AppendText($"//{thoughts.Explanation}{Environment.NewLine}");

            // Reset and Scroll
            richTextBoxAIThoughts.SelectionIndent = 0;
            richTextBoxAIThoughts.AppendText(Environment.NewLine);
            richTextBoxAIThoughts.ScrollToCaret();
        }

        private async void buttonStrudelLogin_Click(object sender, EventArgs e)
        {
            buttonStrudelLogin.Enabled = false;
            buttonStrudelLogin.Image = Properties.Resources.icons8_hourglass_50;

            var res = await PerformanceService.Login();

            if (res)
            {
                buttonStrudelLogin.Image = Properties.Resources.icons8_success_48;
            }
            else
            {
                buttonStrudelLogin.Image = Properties.Resources.icons8_retry_50;
                buttonStrudelLogin.Enabled = true;
            }
        }

        private void buttonLaunchStrudel_Click(object sender, EventArgs e)
        {
            Log.Info("Attempting to launch strudel");
            StrudelLauncher.Launch();
        }
    }
    #endregion
}

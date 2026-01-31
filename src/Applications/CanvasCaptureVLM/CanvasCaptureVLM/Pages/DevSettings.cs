using CanvasCaptureVLM.Classes.Settings;

namespace CanvasCaptureVLM.Pages
{
    public partial class DevSettings : Form
    {
        private readonly SettingsService SettingsService;
        internal DevSettings(SettingsService settingsService)
        {
            InitializeComponent();
            SettingsService = settingsService;
            PageSetup();
        }

        private void DevSettings_Load(object sender, EventArgs e)
        {
        }

        private void PageSetup()
        {
            labelVersion.Text = SettingsService.Settings.ApplicationVersion;
            checkBoxShowLogs.Checked = SettingsService.Settings.ShowLogs;
            checkBoxSaveLogs.Checked = SettingsService.Settings.SaveLogs;
            checkBoxSendPrompt.Checked = SettingsService.Settings.SendPrompt;
            labelImageDirectory.Text = SettingsService.Settings.ImageDirectory;

            SetUpSecuredSettingsFields();

            textBoxApiKey.Text = SettingsService.Settings.APIKey;
            textBoxStrudelEmail.Text = SettingsService.Settings.StrudelEmail;
            textBoxStrudelPassword.Text = SettingsService.Settings.StrudelPassword;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
           
            SettingsService.Settings.ShowLogs = checkBoxShowLogs.Checked;
            SettingsService.Settings.SaveLogs = checkBoxSaveLogs.Checked;
            SettingsService.Settings.SendPrompt = checkBoxSendPrompt.Checked;
            SettingsService.Settings.MusicGenre = textBoxMusicGenre.Text;
            
            if (SettingsService.Settings.APIKey != textBoxApiKey.Text)
            {
                SettingsService.Settings.APIKey = textBoxApiKey.Text;
            }

            if (SettingsService.Settings.StrudelEmail != textBoxStrudelEmail.Text)
            {
                SettingsService.Settings.StrudelEmail = textBoxStrudelEmail.Text;
            }

            if (SettingsService.Settings.StrudelPassword != textBoxStrudelPassword.Text)
            {
                SettingsService.Settings.StrudelPassword = textBoxStrudelPassword.Text;
            }

            MessageBox.Show("Settings saved successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DevSettings_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible) return;
            PageSetup();
        }

        private void SetUpSecuredSettingsFields()
        {
            bool isApiKeyEmpty = string.IsNullOrEmpty(SettingsService.Settings.APIKey);
            bool isStrudelPasswordEmpty = string.IsNullOrEmpty(SettingsService.Settings.StrudelPassword);

            if (!isApiKeyEmpty)
            {
                textBoxApiKey.Enabled = false;
            }

            if (!isStrudelPasswordEmpty)
            {
                textBoxStrudelPassword.Enabled = false;
            }
        }
    }
}

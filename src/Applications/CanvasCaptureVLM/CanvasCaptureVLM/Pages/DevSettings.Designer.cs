namespace CanvasCaptureVLM.Pages
{
    partial class DevSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxAppSettings = new GroupBox();
            textBoxStrudelPassword = new TextBox();
            textBoxStrudelEmail = new TextBox();
            label1 = new Label();
            textBoxApiKey = new TextBox();
            labelApiKeyLabel = new Label();
            labelImageDirectory = new Label();
            labelImgDirectoryLabel = new Label();
            labelVersion = new Label();
            labelVersionLabel = new Label();
            groupBoxDevSettings = new GroupBox();
            checkBoxSendPrompt = new CheckBox();
            checkBoxSaveLogs = new CheckBox();
            checkBoxShowLogs = new CheckBox();
            groupBoxPerformanceSettings = new GroupBox();
            textBoxMusicGenre = new TextBox();
            labelMusicGenre = new Label();
            buttonSave = new Button();
            groupBoxAppSettings.SuspendLayout();
            groupBoxDevSettings.SuspendLayout();
            groupBoxPerformanceSettings.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxAppSettings
            // 
            groupBoxAppSettings.Controls.Add(textBoxStrudelPassword);
            groupBoxAppSettings.Controls.Add(textBoxStrudelEmail);
            groupBoxAppSettings.Controls.Add(label1);
            groupBoxAppSettings.Controls.Add(textBoxApiKey);
            groupBoxAppSettings.Controls.Add(labelApiKeyLabel);
            groupBoxAppSettings.Controls.Add(labelImageDirectory);
            groupBoxAppSettings.Controls.Add(labelImgDirectoryLabel);
            groupBoxAppSettings.Controls.Add(labelVersion);
            groupBoxAppSettings.Controls.Add(labelVersionLabel);
            groupBoxAppSettings.ForeColor = Color.FromArgb(225, 225, 225);
            groupBoxAppSettings.Location = new Point(12, 12);
            groupBoxAppSettings.Name = "groupBoxAppSettings";
            groupBoxAppSettings.Size = new Size(636, 198);
            groupBoxAppSettings.TabIndex = 0;
            groupBoxAppSettings.TabStop = false;
            groupBoxAppSettings.Text = "Application Settings";
            // 
            // textBoxStrudelPassword
            // 
            textBoxStrudelPassword.Location = new Point(405, 156);
            textBoxStrudelPassword.Name = "textBoxStrudelPassword";
            textBoxStrudelPassword.PlaceholderText = "Password";
            textBoxStrudelPassword.Size = new Size(210, 31);
            textBoxStrudelPassword.TabIndex = 9;
            textBoxStrudelPassword.UseSystemPasswordChar = true;
            // 
            // textBoxStrudelEmail
            // 
            textBoxStrudelEmail.Location = new Point(213, 156);
            textBoxStrudelEmail.Name = "textBoxStrudelEmail";
            textBoxStrudelEmail.PlaceholderText = "Email";
            textBoxStrudelEmail.Size = new Size(186, 31);
            textBoxStrudelEmail.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 159);
            label1.Name = "label1";
            label1.Size = new Size(168, 25);
            label1.TabIndex = 7;
            label1.Text = "Strudel Credentials: ";
            // 
            // textBoxApiKey
            // 
            textBoxApiKey.Location = new Point(213, 113);
            textBoxApiKey.Name = "textBoxApiKey";
            textBoxApiKey.Size = new Size(402, 31);
            textBoxApiKey.TabIndex = 6;
            textBoxApiKey.UseSystemPasswordChar = true;
            // 
            // labelApiKeyLabel
            // 
            labelApiKeyLabel.AutoSize = true;
            labelApiKeyLabel.Location = new Point(6, 116);
            labelApiKeyLabel.Name = "labelApiKeyLabel";
            labelApiKeyLabel.Size = new Size(81, 25);
            labelApiKeyLabel.TabIndex = 5;
            labelApiKeyLabel.Text = "API Key: ";
            // 
            // labelImageDirectory
            // 
            labelImageDirectory.AutoSize = true;
            labelImageDirectory.Location = new Point(213, 77);
            labelImageDirectory.Name = "labelImageDirectory";
            labelImageDirectory.Size = new Size(72, 25);
            labelImageDirectory.TabIndex = 4;
            labelImageDirectory.Text = "FilePath";
            // 
            // labelImgDirectoryLabel
            // 
            labelImgDirectoryLabel.AutoSize = true;
            labelImgDirectoryLabel.Location = new Point(6, 77);
            labelImgDirectoryLabel.Name = "labelImgDirectoryLabel";
            labelImgDirectoryLabel.Size = new Size(143, 25);
            labelImgDirectoryLabel.TabIndex = 4;
            labelImgDirectoryLabel.Text = "Image Directory:";
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(213, 40);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(70, 25);
            labelVersion.TabIndex = 1;
            labelVersion.Text = "Version";
            // 
            // labelVersionLabel
            // 
            labelVersionLabel.AutoSize = true;
            labelVersionLabel.ForeColor = Color.FromArgb(230, 230, 230);
            labelVersionLabel.Location = new Point(6, 40);
            labelVersionLabel.Name = "labelVersionLabel";
            labelVersionLabel.Size = new Size(169, 25);
            labelVersionLabel.TabIndex = 0;
            labelVersionLabel.Text = "Application Version:";
            // 
            // groupBoxDevSettings
            // 
            groupBoxDevSettings.Controls.Add(checkBoxSendPrompt);
            groupBoxDevSettings.Controls.Add(checkBoxSaveLogs);
            groupBoxDevSettings.Controls.Add(checkBoxShowLogs);
            groupBoxDevSettings.ForeColor = Color.FromArgb(225, 225, 225);
            groupBoxDevSettings.Location = new Point(12, 216);
            groupBoxDevSettings.Name = "groupBoxDevSettings";
            groupBoxDevSettings.Size = new Size(636, 157);
            groupBoxDevSettings.TabIndex = 1;
            groupBoxDevSettings.TabStop = false;
            groupBoxDevSettings.Text = "Developer Settings";
            // 
            // checkBoxSendPrompt
            // 
            checkBoxSendPrompt.AutoSize = true;
            checkBoxSendPrompt.Checked = true;
            checkBoxSendPrompt.CheckState = CheckState.Checked;
            checkBoxSendPrompt.Location = new Point(6, 109);
            checkBoxSendPrompt.Name = "checkBoxSendPrompt";
            checkBoxSendPrompt.Size = new Size(121, 29);
            checkBoxSendPrompt.TabIndex = 2;
            checkBoxSendPrompt.Text = "Play Music";
            checkBoxSendPrompt.UseVisualStyleBackColor = true;
            // 
            // checkBoxSaveLogs
            // 
            checkBoxSaveLogs.AutoSize = true;
            checkBoxSaveLogs.Checked = true;
            checkBoxSaveLogs.CheckState = CheckState.Checked;
            checkBoxSaveLogs.Location = new Point(6, 75);
            checkBoxSaveLogs.Name = "checkBoxSaveLogs";
            checkBoxSaveLogs.Size = new Size(118, 29);
            checkBoxSaveLogs.TabIndex = 1;
            checkBoxSaveLogs.Text = "Log to file";
            checkBoxSaveLogs.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowLogs
            // 
            checkBoxShowLogs.AutoSize = true;
            checkBoxShowLogs.Checked = true;
            checkBoxShowLogs.CheckState = CheckState.Checked;
            checkBoxShowLogs.Location = new Point(6, 41);
            checkBoxShowLogs.Name = "checkBoxShowLogs";
            checkBoxShowLogs.Size = new Size(125, 29);
            checkBoxShowLogs.TabIndex = 0;
            checkBoxShowLogs.Text = "Show Logs";
            checkBoxShowLogs.UseVisualStyleBackColor = true;
            // 
            // groupBoxPerformanceSettings
            // 
            groupBoxPerformanceSettings.Controls.Add(textBoxMusicGenre);
            groupBoxPerformanceSettings.Controls.Add(labelMusicGenre);
            groupBoxPerformanceSettings.ForeColor = Color.FromArgb(225, 225, 225);
            groupBoxPerformanceSettings.Location = new Point(10, 382);
            groupBoxPerformanceSettings.Name = "groupBoxPerformanceSettings";
            groupBoxPerformanceSettings.Size = new Size(636, 97);
            groupBoxPerformanceSettings.TabIndex = 2;
            groupBoxPerformanceSettings.TabStop = false;
            groupBoxPerformanceSettings.Text = "Performance Settings";
            // 
            // textBoxMusicGenre
            // 
            textBoxMusicGenre.Location = new Point(215, 44);
            textBoxMusicGenre.Name = "textBoxMusicGenre";
            textBoxMusicGenre.Size = new Size(402, 31);
            textBoxMusicGenre.TabIndex = 1;
            // 
            // labelMusicGenre
            // 
            labelMusicGenre.AutoSize = true;
            labelMusicGenre.Location = new Point(6, 47);
            labelMusicGenre.Name = "labelMusicGenre";
            labelMusicGenre.Size = new Size(186, 25);
            labelMusicGenre.TabIndex = 0;
            labelMusicGenre.Text = "Override Music Genre:";
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(479, 490);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(167, 49);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // DevSettings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1026, 593);
            Controls.Add(buttonSave);
            Controls.Add(groupBoxPerformanceSettings);
            Controls.Add(groupBoxDevSettings);
            Controls.Add(groupBoxAppSettings);
            FormBorderStyle = FormBorderStyle.None;
            Name = "DevSettings";
            Text = "DevSettings";
            VisibleChanged += DevSettings_VisibleChanged;
            groupBoxAppSettings.ResumeLayout(false);
            groupBoxAppSettings.PerformLayout();
            groupBoxDevSettings.ResumeLayout(false);
            groupBoxDevSettings.PerformLayout();
            groupBoxPerformanceSettings.ResumeLayout(false);
            groupBoxPerformanceSettings.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxAppSettings;
        private GroupBox groupBoxDevSettings;
        private GroupBox groupBoxPerformanceSettings;
        private Label labelVersionLabel;
        private Label labelVersion;
        private CheckBox checkBoxSendPrompt;
        private CheckBox checkBoxSaveLogs;
        private CheckBox checkBoxShowLogs;
        private TextBox textBoxMusicGenre;
        private Label labelMusicGenre;
        private Button buttonSave;
        private Label labelImgDirectoryLabel;
        private Label labelImageDirectory;
        private Label labelApiKeyLabel;
        private TextBox textBoxApiKey;
        private TextBox textBoxStrudelPassword;
        private TextBox textBoxStrudelEmail;
        private Label label1;
    }
}
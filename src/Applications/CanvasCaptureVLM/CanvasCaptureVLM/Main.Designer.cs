namespace CanvasCaptureVLM
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelTop = new Panel();
            labelTokenUsage = new Label();
            labelTokenUsageLabel = new Label();
            pictureBoxHamburgerBtn = new PictureBox();
            flowLayoutPanelSideBar = new FlowLayoutPanel();
            panelPerformanceBtn = new Panel();
            buttonPerformance = new Button();
            panelSettingsBtn = new Panel();
            buttonDevSettings = new Button();
            panelAboutBtn = new Panel();
            buttonAbout = new Button();
            timerSideBarTransition = new System.Windows.Forms.Timer(components);
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxHamburgerBtn).BeginInit();
            flowLayoutPanelSideBar.SuspendLayout();
            panelPerformanceBtn.SuspendLayout();
            panelSettingsBtn.SuspendLayout();
            panelAboutBtn.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(25, 25, 26);
            panelTop.Controls.Add(labelTokenUsage);
            panelTop.Controls.Add(labelTokenUsageLabel);
            panelTop.Controls.Add(pictureBoxHamburgerBtn);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1715, 74);
            panelTop.TabIndex = 0;
            // 
            // labelTokenUsage
            // 
            labelTokenUsage.AutoSize = true;
            labelTokenUsage.ForeColor = Color.Transparent;
            labelTokenUsage.Location = new Point(1629, 22);
            labelTokenUsage.Name = "labelTokenUsage";
            labelTokenUsage.Size = new Size(22, 25);
            labelTokenUsage.TabIndex = 3;
            labelTokenUsage.Text = "0";
            // 
            // labelTokenUsageLabel
            // 
            labelTokenUsageLabel.AutoSize = true;
            labelTokenUsageLabel.ForeColor = Color.Transparent;
            labelTokenUsageLabel.Location = new Point(1477, 22);
            labelTokenUsageLabel.Name = "labelTokenUsageLabel";
            labelTokenUsageLabel.Size = new Size(116, 25);
            labelTokenUsageLabel.TabIndex = 2;
            labelTokenUsageLabel.Text = "Token Usage:";
            // 
            // pictureBoxHamburgerBtn
            // 
            pictureBoxHamburgerBtn.Image = Properties.Resources.icons8_menu_32;
            pictureBoxHamburgerBtn.Location = new Point(12, 12);
            pictureBoxHamburgerBtn.Name = "pictureBoxHamburgerBtn";
            pictureBoxHamburgerBtn.Size = new Size(50, 46);
            pictureBoxHamburgerBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxHamburgerBtn.TabIndex = 1;
            pictureBoxHamburgerBtn.TabStop = false;
            pictureBoxHamburgerBtn.Click += pictureBoxHamburgerBtn_Click;
            // 
            // flowLayoutPanelSideBar
            // 
            flowLayoutPanelSideBar.BackColor = Color.FromArgb(25, 25, 26);
            flowLayoutPanelSideBar.Controls.Add(panelPerformanceBtn);
            flowLayoutPanelSideBar.Controls.Add(panelSettingsBtn);
            flowLayoutPanelSideBar.Controls.Add(panelAboutBtn);
            flowLayoutPanelSideBar.Dock = DockStyle.Left;
            flowLayoutPanelSideBar.Location = new Point(0, 74);
            flowLayoutPanelSideBar.Name = "flowLayoutPanelSideBar";
            flowLayoutPanelSideBar.Size = new Size(247, 1079);
            flowLayoutPanelSideBar.TabIndex = 1;
            // 
            // panelPerformanceBtn
            // 
            panelPerformanceBtn.Controls.Add(buttonPerformance);
            panelPerformanceBtn.Location = new Point(3, 3);
            panelPerformanceBtn.Name = "panelPerformanceBtn";
            panelPerformanceBtn.Size = new Size(358, 67);
            panelPerformanceBtn.TabIndex = 3;
            // 
            // buttonPerformance
            // 
            buttonPerformance.BackColor = Color.FromArgb(25, 25, 26);
            buttonPerformance.Dock = DockStyle.Left;
            buttonPerformance.FlatAppearance.BorderSize = 0;
            buttonPerformance.FlatStyle = FlatStyle.Flat;
            buttonPerformance.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonPerformance.ForeColor = Color.FromArgb(230, 230, 230);
            buttonPerformance.Image = Properties.Resources.icons8_paint_brush_50;
            buttonPerformance.ImageAlign = ContentAlignment.MiddleLeft;
            buttonPerformance.Location = new Point(0, 0);
            buttonPerformance.Name = "buttonPerformance";
            buttonPerformance.Padding = new Padding(3);
            buttonPerformance.Size = new Size(272, 67);
            buttonPerformance.TabIndex = 2;
            buttonPerformance.Text = "Performance";
            buttonPerformance.UseVisualStyleBackColor = false;
            buttonPerformance.Click += buttonPerformance_Click;
            // 
            // panelSettingsBtn
            // 
            panelSettingsBtn.Controls.Add(buttonDevSettings);
            panelSettingsBtn.Location = new Point(3, 76);
            panelSettingsBtn.Name = "panelSettingsBtn";
            panelSettingsBtn.Size = new Size(358, 67);
            panelSettingsBtn.TabIndex = 2;
            // 
            // buttonDevSettings
            // 
            buttonDevSettings.BackColor = Color.FromArgb(25, 25, 26);
            buttonDevSettings.Dock = DockStyle.Left;
            buttonDevSettings.FlatAppearance.BorderSize = 0;
            buttonDevSettings.FlatStyle = FlatStyle.Flat;
            buttonDevSettings.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonDevSettings.ForeColor = Color.FromArgb(230, 230, 230);
            buttonDevSettings.Image = Properties.Resources.icons8_development_skill_30;
            buttonDevSettings.ImageAlign = ContentAlignment.MiddleLeft;
            buttonDevSettings.Location = new Point(0, 0);
            buttonDevSettings.Name = "buttonDevSettings";
            buttonDevSettings.Padding = new Padding(3);
            buttonDevSettings.Size = new Size(272, 67);
            buttonDevSettings.TabIndex = 3;
            buttonDevSettings.Text = "Dev Settings";
            buttonDevSettings.UseVisualStyleBackColor = false;
            buttonDevSettings.Click += buttonDevSettings_Click;
            // 
            // panelAboutBtn
            // 
            panelAboutBtn.Controls.Add(buttonAbout);
            panelAboutBtn.Location = new Point(3, 149);
            panelAboutBtn.Name = "panelAboutBtn";
            panelAboutBtn.Size = new Size(358, 67);
            panelAboutBtn.TabIndex = 3;
            // 
            // buttonAbout
            // 
            buttonAbout.BackColor = Color.FromArgb(25, 25, 26);
            buttonAbout.Dock = DockStyle.Left;
            buttonAbout.FlatAppearance.BorderSize = 0;
            buttonAbout.FlatStyle = FlatStyle.Flat;
            buttonAbout.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonAbout.ForeColor = Color.FromArgb(230, 230, 230);
            buttonAbout.Image = Properties.Resources.icons8_about_30;
            buttonAbout.ImageAlign = ContentAlignment.MiddleLeft;
            buttonAbout.Location = new Point(0, 0);
            buttonAbout.Name = "buttonAbout";
            buttonAbout.Padding = new Padding(3);
            buttonAbout.Size = new Size(272, 67);
            buttonAbout.TabIndex = 3;
            buttonAbout.Text = "About";
            buttonAbout.UseVisualStyleBackColor = false;
            buttonAbout.Click += buttonAbout_Click;
            // 
            // timerSideBarTransition
            // 
            timerSideBarTransition.Interval = 10;
            timerSideBarTransition.Tick += timerSideBarTransition_Tick;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1715, 1153);
            Controls.Add(flowLayoutPanelSideBar);
            Controls.Add(panelTop);
            IsMdiContainer = true;
            Name = "FormMain";
            Text = "Painting Music - Canvas Capture VLM";
            Load += FormMain_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxHamburgerBtn).EndInit();
            flowLayoutPanelSideBar.ResumeLayout(false);
            panelPerformanceBtn.ResumeLayout(false);
            panelSettingsBtn.ResumeLayout(false);
            panelAboutBtn.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private PictureBox pictureBoxHamburgerBtn;
        private FlowLayoutPanel flowLayoutPanelSideBar;
        private Button buttonPerformance;
        private Panel panelPerformanceBtn;
        private Panel panelSettingsBtn;
        private Button buttonDevSettings;
        private Panel panelAboutBtn;
        private Button buttonAbout;
        private System.Windows.Forms.Timer timerSideBarTransition;
        private Label labelTokenUsageLabel;
        private Label labelTokenUsage;
    }
}

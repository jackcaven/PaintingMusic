namespace CanvasCaptureVLM.Pages
{
    partial class Performance
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
            panelControls = new Panel();
            buttonStrudelLogin = new Button();
            labelStrudelStatus = new Label();
            buttonStop = new Button();
            buttonStart = new Button();
            textBoxDevLogs = new TextBox();
            richTextBoxAIThoughts = new RichTextBox();
            pictureBoxCanvas = new PictureBox();
            panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCanvas).BeginInit();
            SuspendLayout();
            // 
            // panelControls
            // 
            panelControls.BackColor = Color.FromArgb(25, 25, 26);
            panelControls.Controls.Add(buttonStrudelLogin);
            panelControls.Controls.Add(labelStrudelStatus);
            panelControls.Controls.Add(buttonStop);
            panelControls.Controls.Add(buttonStart);
            panelControls.Dock = DockStyle.Top;
            panelControls.Location = new Point(0, 0);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(1688, 83);
            panelControls.TabIndex = 0;
            // 
            // buttonStrudelLogin
            // 
            buttonStrudelLogin.BackColor = Color.FromArgb(25, 25, 25);
            buttonStrudelLogin.BackgroundImageLayout = ImageLayout.Stretch;
            buttonStrudelLogin.Image = Properties.Resources.icons8_login_24;
            buttonStrudelLogin.Location = new Point(1277, 14);
            buttonStrudelLogin.Name = "buttonStrudelLogin";
            buttonStrudelLogin.Size = new Size(62, 57);
            buttonStrudelLogin.TabIndex = 3;
            buttonStrudelLogin.UseVisualStyleBackColor = false;
            buttonStrudelLogin.Click += buttonStrudelLogin_Click;
            // 
            // labelStrudelStatus
            // 
            labelStrudelStatus.AutoSize = true;
            labelStrudelStatus.ForeColor = SystemColors.ButtonHighlight;
            labelStrudelStatus.Location = new Point(1142, 28);
            labelStrudelStatus.Name = "labelStrudelStatus";
            labelStrudelStatus.Size = new Size(124, 25);
            labelStrudelStatus.TabIndex = 2;
            labelStrudelStatus.Text = "Strudel Status:";
            // 
            // buttonStop
            // 
            buttonStop.BackColor = Color.FromArgb(201, 85, 85);
            buttonStop.BackgroundImageLayout = ImageLayout.None;
            buttonStop.ForeColor = Color.FromArgb(230, 230, 230);
            buttonStop.Image = Properties.Resources.icons8_stop_30;
            buttonStop.ImageAlign = ContentAlignment.MiddleLeft;
            buttonStop.Location = new Point(164, 12);
            buttonStop.Name = "buttonStop";
            buttonStop.Padding = new Padding(1);
            buttonStop.Size = new Size(98, 57);
            buttonStop.TabIndex = 1;
            buttonStop.Text = "Stop";
            buttonStop.TextAlign = ContentAlignment.MiddleRight;
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // buttonStart
            // 
            buttonStart.BackColor = Color.FromArgb(63, 169, 107);
            buttonStart.BackgroundImageLayout = ImageLayout.None;
            buttonStart.ForeColor = Color.FromArgb(230, 230, 230);
            buttonStart.Image = Properties.Resources.icons8_start_30;
            buttonStart.ImageAlign = ContentAlignment.MiddleLeft;
            buttonStart.Location = new Point(12, 12);
            buttonStart.Name = "buttonStart";
            buttonStart.Padding = new Padding(1);
            buttonStart.Size = new Size(98, 57);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start";
            buttonStart.TextAlign = ContentAlignment.MiddleRight;
            buttonStart.UseVisualStyleBackColor = false;
            buttonStart.Click += buttonStart_Click;
            // 
            // textBoxDevLogs
            // 
            textBoxDevLogs.BackColor = Color.FromArgb(25, 25, 26);
            textBoxDevLogs.BorderStyle = BorderStyle.None;
            textBoxDevLogs.Dock = DockStyle.Bottom;
            textBoxDevLogs.ForeColor = Color.FromArgb(224, 224, 224);
            textBoxDevLogs.Location = new Point(0, 640);
            textBoxDevLogs.Multiline = true;
            textBoxDevLogs.Name = "textBoxDevLogs";
            textBoxDevLogs.ReadOnly = true;
            textBoxDevLogs.ScrollBars = ScrollBars.Vertical;
            textBoxDevLogs.Size = new Size(1688, 187);
            textBoxDevLogs.TabIndex = 3;
            // 
            // richTextBoxAIThoughts
            // 
            richTextBoxAIThoughts.BackColor = Color.FromArgb(25, 25, 26);
            richTextBoxAIThoughts.BorderStyle = BorderStyle.None;
            richTextBoxAIThoughts.Dock = DockStyle.Right;
            richTextBoxAIThoughts.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            richTextBoxAIThoughts.ForeColor = Color.Black;
            richTextBoxAIThoughts.Location = new Point(1046, 83);
            richTextBoxAIThoughts.Name = "richTextBoxAIThoughts";
            richTextBoxAIThoughts.Size = new Size(642, 557);
            richTextBoxAIThoughts.TabIndex = 4;
            richTextBoxAIThoughts.Text = "";
            // 
            // pictureBoxCanvas
            // 
            pictureBoxCanvas.Dock = DockStyle.Left;
            pictureBoxCanvas.Location = new Point(0, 83);
            pictureBoxCanvas.Name = "pictureBoxCanvas";
            pictureBoxCanvas.Size = new Size(1040, 557);
            pictureBoxCanvas.TabIndex = 5;
            pictureBoxCanvas.TabStop = false;
            // 
            // Performance
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1688, 827);
            Controls.Add(richTextBoxAIThoughts);
            Controls.Add(pictureBoxCanvas);
            Controls.Add(textBoxDevLogs);
            Controls.Add(panelControls);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Performance";
            Text = "Performance";
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCanvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelControls;
        private Button buttonStart;
        private Button buttonStop;
        private TextBox textBoxDevLogs;
        private RichTextBox richTextBoxAIThoughts;
        private PictureBox pictureBoxCanvas;
        private Label labelStrudelStatus;
        private Button buttonStrudelLogin;
    }
}
namespace CanvasCaptureVLM.Pages
{
    partial class About
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
            pictureBoxPM = new PictureBox();
            textBoxDescription = new TextBox();
            textBoxRecognition = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPM).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxPM
            // 
            pictureBoxPM.Anchor = AnchorStyles.None;
            pictureBoxPM.Image = Properties.Resources.pm_logo;
            pictureBoxPM.Location = new Point(298, 15);
            pictureBoxPM.Margin = new Padding(4, 4, 4, 4);
            pictureBoxPM.Name = "pictureBoxPM";
            pictureBoxPM.Size = new Size(425, 252);
            pictureBoxPM.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPM.TabIndex = 0;
            pictureBoxPM.TabStop = false;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Anchor = AnchorStyles.None;
            textBoxDescription.BackColor = Color.FromArgb(30, 30, 30);
            textBoxDescription.BorderStyle = BorderStyle.None;
            textBoxDescription.ForeColor = Color.FromArgb(230, 230, 230);
            textBoxDescription.Location = new Point(217, 298);
            textBoxDescription.Margin = new Padding(4, 4, 4, 4);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.Size = new Size(616, 79);
            textBoxDescription.TabIndex = 1;
            textBoxDescription.Text = "Canvas Capture VLM is a collaborative product between Painting Music and Dr Harry Whalley";
            textBoxDescription.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxRecognition
            // 
            textBoxRecognition.BackColor = Color.FromArgb(30, 30, 30);
            textBoxRecognition.BorderStyle = BorderStyle.None;
            textBoxRecognition.Dock = DockStyle.Bottom;
            textBoxRecognition.ForeColor = Color.FromArgb(230, 230, 230);
            textBoxRecognition.Location = new Point(0, 532);
            textBoxRecognition.Margin = new Padding(4, 4, 4, 4);
            textBoxRecognition.Multiline = true;
            textBoxRecognition.Name = "textBoxRecognition";
            textBoxRecognition.ReadOnly = true;
            textBoxRecognition.Size = new Size(1040, 44);
            textBoxRecognition.TabIndex = 2;
            textBoxRecognition.Text = "Icons brought to you by Icons8.  VLM provided by OpenAI.\r\n";
            textBoxRecognition.TextAlign = HorizontalAlignment.Center;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1040, 576);
            Controls.Add(textBoxRecognition);
            Controls.Add(textBoxDescription);
            Controls.Add(pictureBoxPM);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 4, 4, 4);
            Name = "About";
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)pictureBoxPM).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxPM;
        private TextBox textBoxDescription;
        private TextBox textBoxRecognition;
    }
}
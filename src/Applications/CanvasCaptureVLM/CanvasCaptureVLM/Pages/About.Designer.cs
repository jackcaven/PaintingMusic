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
            pictureBoxPM.Location = new Point(229, 12);
            pictureBoxPM.Name = "pictureBoxPM";
            pictureBoxPM.Size = new Size(327, 197);
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
            textBoxDescription.Location = new Point(167, 233);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.Size = new Size(474, 62);
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
            textBoxRecognition.Location = new Point(0, 416);
            textBoxRecognition.Multiline = true;
            textBoxRecognition.Name = "textBoxRecognition";
            textBoxRecognition.ReadOnly = true;
            textBoxRecognition.Size = new Size(800, 34);
            textBoxRecognition.TabIndex = 2;
            textBoxRecognition.Text = "Icons brought to you by Icons8.  VLM provided by Ollama";
            textBoxRecognition.TextAlign = HorizontalAlignment.Center;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(800, 450);
            Controls.Add(textBoxRecognition);
            Controls.Add(textBoxDescription);
            Controls.Add(pictureBoxPM);
            FormBorderStyle = FormBorderStyle.None;
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
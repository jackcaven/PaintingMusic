namespace CanvasCapture
{
    partial class CanvasCaptureMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanvasCaptureMain));
            panelControls = new Panel();
            labelInstrument = new Label();
            comboBoxInstruments = new ComboBox();
            label1 = new Label();
            checkedListBoxOptions = new CheckedListBox();
            buttonStop = new Button();
            buttonStart = new Button();
            pictureBoxImages = new PictureBox();
            richTextBoxDataViewer = new RichTextBox();
            panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImages).BeginInit();
            SuspendLayout();
            // 
            // panelControls
            // 
            panelControls.BorderStyle = BorderStyle.Fixed3D;
            panelControls.Controls.Add(labelInstrument);
            panelControls.Controls.Add(comboBoxInstruments);
            panelControls.Controls.Add(label1);
            panelControls.Controls.Add(checkedListBoxOptions);
            panelControls.Controls.Add(buttonStop);
            panelControls.Controls.Add(buttonStart);
            panelControls.Location = new Point(12, 12);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(921, 467);
            panelControls.TabIndex = 0;
            // 
            // labelInstrument
            // 
            labelInstrument.AutoSize = true;
            labelInstrument.Location = new Point(17, 273);
            labelInstrument.Name = "labelInstrument";
            labelInstrument.Size = new Size(130, 32);
            labelInstrument.TabIndex = 6;
            labelInstrument.Text = "Instrument";
            // 
            // comboBoxInstruments
            // 
            comboBoxInstruments.FormattingEnabled = true;
            comboBoxInstruments.Location = new Point(172, 270);
            comboBoxInstruments.Name = "comboBoxInstruments";
            comboBoxInstruments.Size = new Size(724, 40);
            comboBoxInstruments.TabIndex = 5;
            comboBoxInstruments.SelectedIndexChanged += comboBoxInstruments_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 16);
            label1.Name = "label1";
            label1.Size = new Size(215, 32);
            label1.TabIndex = 4;
            label1.Text = "Developer Options";
            // 
            // checkedListBoxOptions
            // 
            checkedListBoxOptions.BorderStyle = BorderStyle.None;
            checkedListBoxOptions.Font = new Font("Segoe UI", 10F);
            checkedListBoxOptions.FormattingEnabled = true;
            checkedListBoxOptions.Location = new Point(17, 62);
            checkedListBoxOptions.Name = "checkedListBoxOptions";
            checkedListBoxOptions.Size = new Size(879, 160);
            checkedListBoxOptions.TabIndex = 3;
            checkedListBoxOptions.ItemCheck += checkedListBoxOptions_ItemCheck;
            // 
            // buttonStop
            // 
            buttonStop.BackColor = Color.DimGray;
            buttonStop.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonStop.ForeColor = Color.Crimson;
            buttonStop.Location = new Point(686, 359);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(210, 77);
            buttonStop.TabIndex = 2;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // buttonStart
            // 
            buttonStart.BackColor = Color.DimGray;
            buttonStart.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonStart.Location = new Point(17, 347);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(210, 74);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = false;
            buttonStart.Click += buttonStart_Click;
            // 
            // pictureBoxImages
            // 
            pictureBoxImages.Location = new Point(945, 12);
            pictureBoxImages.Name = "pictureBoxImages";
            pictureBoxImages.Size = new Size(1920, 1268);
            pictureBoxImages.TabIndex = 1;
            pictureBoxImages.TabStop = false;
            // 
            // richTextBoxDataViewer
            // 
            richTextBoxDataViewer.BackColor = SystemColors.InfoText;
            richTextBoxDataViewer.Font = new Font("Segoe UI", 10F);
            richTextBoxDataViewer.ForeColor = Color.Cyan;
            richTextBoxDataViewer.Location = new Point(12, 503);
            richTextBoxDataViewer.Name = "richTextBoxDataViewer";
            richTextBoxDataViewer.ReadOnly = true;
            richTextBoxDataViewer.Size = new Size(921, 777);
            richTextBoxDataViewer.TabIndex = 2;
            richTextBoxDataViewer.Text = "";
            // 
            // CanvasCaptureMain
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(2879, 1300);
            Controls.Add(richTextBoxDataViewer);
            Controls.Add(pictureBoxImages);
            Controls.Add(panelControls);
            ForeColor = Color.LimeGreen;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CanvasCaptureMain";
            Text = "Painting Music - Canvas Capture";
            WindowState = FormWindowState.Maximized;
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImages).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelControls;
        private Button buttonStart;
        private Button buttonStop;
        private PictureBox pictureBoxImages;
        private RichTextBox richTextBoxDataViewer;
        private CheckedListBox checkedListBoxOptions;
        private Label label1;
        private Label labelInstrument;
        private ComboBox comboBoxInstruments;
    }
}

namespace CanvasCaptureVLM.Pages
{
    partial class PromptDesigner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptDesigner));
            splitContainerPromptRepo = new SplitContainer();
            listBoxPrompts = new ListBox();
            panelPromptList = new Panel();
            labelPromptsList = new Label();
            buttonDeletePrompt = new Button();
            buttonAddPrompt = new Button();
            panelPromptText = new Panel();
            groupBoxPromptText = new GroupBox();
            textBoxPrompt = new TextBox();
            panelPromptDetailsBottomControls = new Panel();
            buttonSavePrompt = new Button();
            panelPromptDetailsTopControls = new Panel();
            groupBoxPromptName = new GroupBox();
            textBoxPromptName = new TextBox();
            panelPromptDetailsTitle = new Panel();
            labelPromptDetails = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainerPromptRepo).BeginInit();
            splitContainerPromptRepo.Panel1.SuspendLayout();
            splitContainerPromptRepo.Panel2.SuspendLayout();
            splitContainerPromptRepo.SuspendLayout();
            panelPromptList.SuspendLayout();
            panelPromptText.SuspendLayout();
            groupBoxPromptText.SuspendLayout();
            panelPromptDetailsBottomControls.SuspendLayout();
            panelPromptDetailsTopControls.SuspendLayout();
            groupBoxPromptName.SuspendLayout();
            panelPromptDetailsTitle.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerPromptRepo
            // 
            splitContainerPromptRepo.Dock = DockStyle.Fill;
            splitContainerPromptRepo.FixedPanel = FixedPanel.Panel1;
            splitContainerPromptRepo.IsSplitterFixed = true;
            splitContainerPromptRepo.Location = new Point(0, 0);
            splitContainerPromptRepo.Margin = new Padding(2);
            splitContainerPromptRepo.Name = "splitContainerPromptRepo";
            // 
            // splitContainerPromptRepo.Panel1
            // 
            splitContainerPromptRepo.Panel1.Controls.Add(listBoxPrompts);
            splitContainerPromptRepo.Panel1.Controls.Add(panelPromptList);
            // 
            // splitContainerPromptRepo.Panel2
            // 
            splitContainerPromptRepo.Panel2.Controls.Add(panelPromptText);
            splitContainerPromptRepo.Panel2.Controls.Add(panelPromptDetailsBottomControls);
            splitContainerPromptRepo.Panel2.Controls.Add(panelPromptDetailsTopControls);
            splitContainerPromptRepo.Panel2.Controls.Add(panelPromptDetailsTitle);
            splitContainerPromptRepo.Size = new Size(1289, 781);
            splitContainerPromptRepo.SplitterDistance = 429;
            splitContainerPromptRepo.SplitterWidth = 15;
            splitContainerPromptRepo.TabIndex = 8;
            // 
            // listBoxPrompts
            // 
            listBoxPrompts.BackColor = Color.FromArgb(30, 30, 30);
            listBoxPrompts.Dock = DockStyle.Fill;
            listBoxPrompts.ForeColor = Color.FromArgb(230, 230, 230);
            listBoxPrompts.FormattingEnabled = true;
            listBoxPrompts.Location = new Point(0, 83);
            listBoxPrompts.Margin = new Padding(2);
            listBoxPrompts.Name = "listBoxPrompts";
            listBoxPrompts.Size = new Size(429, 698);
            listBoxPrompts.TabIndex = 10;
            listBoxPrompts.SelectedIndexChanged += listBoxPrompts_SelectedIndexChanged;
            // 
            // panelPromptList
            // 
            panelPromptList.Controls.Add(labelPromptsList);
            panelPromptList.Controls.Add(buttonDeletePrompt);
            panelPromptList.Controls.Add(buttonAddPrompt);
            panelPromptList.Dock = DockStyle.Top;
            panelPromptList.Location = new Point(0, 0);
            panelPromptList.Margin = new Padding(2);
            panelPromptList.Name = "panelPromptList";
            panelPromptList.Size = new Size(429, 83);
            panelPromptList.TabIndex = 9;
            // 
            // labelPromptsList
            // 
            labelPromptsList.AutoSize = true;
            labelPromptsList.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPromptsList.ForeColor = Color.FromArgb(230, 230, 230);
            labelPromptsList.Location = new Point(18, 24);
            labelPromptsList.Margin = new Padding(2, 0, 2, 0);
            labelPromptsList.Name = "labelPromptsList";
            labelPromptsList.Size = new Size(146, 32);
            labelPromptsList.TabIndex = 8;
            labelPromptsList.Text = "Prompt List";
            // 
            // buttonDeletePrompt
            // 
            buttonDeletePrompt.BackColor = Color.FromArgb(30, 30, 30);
            buttonDeletePrompt.BackgroundImageLayout = ImageLayout.Stretch;
            buttonDeletePrompt.FlatStyle = FlatStyle.Flat;
            buttonDeletePrompt.ForeColor = Color.FromArgb(30, 30, 30);
            buttonDeletePrompt.Image = (Image)resources.GetObject("buttonDeletePrompt.Image");
            buttonDeletePrompt.Location = new Point(357, 16);
            buttonDeletePrompt.Name = "buttonDeletePrompt";
            buttonDeletePrompt.Size = new Size(62, 57);
            buttonDeletePrompt.TabIndex = 7;
            buttonDeletePrompt.UseVisualStyleBackColor = false;
            buttonDeletePrompt.Click += buttonDeletePrompt_Click;
            // 
            // buttonAddPrompt
            // 
            buttonAddPrompt.BackColor = Color.FromArgb(30, 30, 30);
            buttonAddPrompt.BackgroundImageLayout = ImageLayout.Stretch;
            buttonAddPrompt.FlatStyle = FlatStyle.Flat;
            buttonAddPrompt.ForeColor = Color.FromArgb(30, 30, 30);
            buttonAddPrompt.Image = (Image)resources.GetObject("buttonAddPrompt.Image");
            buttonAddPrompt.Location = new Point(288, 16);
            buttonAddPrompt.Name = "buttonAddPrompt";
            buttonAddPrompt.Size = new Size(62, 57);
            buttonAddPrompt.TabIndex = 6;
            buttonAddPrompt.UseVisualStyleBackColor = false;
            buttonAddPrompt.Click += buttonAddPrompt_Click;
            // 
            // panelPromptText
            // 
            panelPromptText.Controls.Add(groupBoxPromptText);
            panelPromptText.Dock = DockStyle.Fill;
            panelPromptText.Location = new Point(0, 206);
            panelPromptText.Margin = new Padding(2);
            panelPromptText.Name = "panelPromptText";
            panelPromptText.Size = new Size(845, 514);
            panelPromptText.TabIndex = 14;
            // 
            // groupBoxPromptText
            // 
            groupBoxPromptText.Controls.Add(textBoxPrompt);
            groupBoxPromptText.Dock = DockStyle.Fill;
            groupBoxPromptText.ForeColor = Color.FromArgb(230, 230, 230);
            groupBoxPromptText.Location = new Point(0, 0);
            groupBoxPromptText.Margin = new Padding(2);
            groupBoxPromptText.Name = "groupBoxPromptText";
            groupBoxPromptText.Padding = new Padding(2);
            groupBoxPromptText.Size = new Size(845, 514);
            groupBoxPromptText.TabIndex = 12;
            groupBoxPromptText.TabStop = false;
            groupBoxPromptText.Text = "Prompt";
            // 
            // textBoxPrompt
            // 
            textBoxPrompt.BackColor = Color.FromArgb(30, 30, 30);
            textBoxPrompt.BorderStyle = BorderStyle.None;
            textBoxPrompt.Dock = DockStyle.Fill;
            textBoxPrompt.ForeColor = Color.FromArgb(230, 230, 230);
            textBoxPrompt.Location = new Point(2, 26);
            textBoxPrompt.Margin = new Padding(2);
            textBoxPrompt.Multiline = true;
            textBoxPrompt.Name = "textBoxPrompt";
            textBoxPrompt.PlaceholderText = "Add prompt text here";
            textBoxPrompt.Size = new Size(841, 486);
            textBoxPrompt.TabIndex = 11;
            // 
            // panelPromptDetailsBottomControls
            // 
            panelPromptDetailsBottomControls.Controls.Add(buttonSavePrompt);
            panelPromptDetailsBottomControls.Dock = DockStyle.Bottom;
            panelPromptDetailsBottomControls.Location = new Point(0, 720);
            panelPromptDetailsBottomControls.Margin = new Padding(2);
            panelPromptDetailsBottomControls.Name = "panelPromptDetailsBottomControls";
            panelPromptDetailsBottomControls.Size = new Size(845, 61);
            panelPromptDetailsBottomControls.TabIndex = 13;
            // 
            // buttonSavePrompt
            // 
            buttonSavePrompt.BackColor = Color.FromArgb(30, 30, 30);
            buttonSavePrompt.BackgroundImageLayout = ImageLayout.Stretch;
            buttonSavePrompt.Dock = DockStyle.Right;
            buttonSavePrompt.FlatStyle = FlatStyle.Flat;
            buttonSavePrompt.ForeColor = Color.FromArgb(230, 230, 230);
            buttonSavePrompt.Location = new Point(702, 0);
            buttonSavePrompt.Name = "buttonSavePrompt";
            buttonSavePrompt.Size = new Size(143, 61);
            buttonSavePrompt.TabIndex = 7;
            buttonSavePrompt.Text = "Save";
            buttonSavePrompt.UseVisualStyleBackColor = false;
            buttonSavePrompt.Click += buttonSavePrompt_Click;
            // 
            // panelPromptDetailsTopControls
            // 
            panelPromptDetailsTopControls.Controls.Add(groupBoxPromptName);
            panelPromptDetailsTopControls.Dock = DockStyle.Top;
            panelPromptDetailsTopControls.Location = new Point(0, 83);
            panelPromptDetailsTopControls.Margin = new Padding(2);
            panelPromptDetailsTopControls.Name = "panelPromptDetailsTopControls";
            panelPromptDetailsTopControls.Size = new Size(845, 123);
            panelPromptDetailsTopControls.TabIndex = 12;
            // 
            // groupBoxPromptName
            // 
            groupBoxPromptName.Controls.Add(textBoxPromptName);
            groupBoxPromptName.Dock = DockStyle.Fill;
            groupBoxPromptName.ForeColor = Color.FromArgb(230, 230, 230);
            groupBoxPromptName.Location = new Point(0, 0);
            groupBoxPromptName.Margin = new Padding(2);
            groupBoxPromptName.Name = "groupBoxPromptName";
            groupBoxPromptName.Padding = new Padding(2);
            groupBoxPromptName.Size = new Size(845, 123);
            groupBoxPromptName.TabIndex = 1;
            groupBoxPromptName.TabStop = false;
            groupBoxPromptName.Text = "Prompt Name";
            // 
            // textBoxPromptName
            // 
            textBoxPromptName.Location = new Point(5, 48);
            textBoxPromptName.Margin = new Padding(2);
            textBoxPromptName.Name = "textBoxPromptName";
            textBoxPromptName.PlaceholderText = "Prompt Name";
            textBoxPromptName.Size = new Size(314, 31);
            textBoxPromptName.TabIndex = 0;
            // 
            // panelPromptDetailsTitle
            // 
            panelPromptDetailsTitle.Controls.Add(labelPromptDetails);
            panelPromptDetailsTitle.Dock = DockStyle.Top;
            panelPromptDetailsTitle.Location = new Point(0, 0);
            panelPromptDetailsTitle.Margin = new Padding(2);
            panelPromptDetailsTitle.Name = "panelPromptDetailsTitle";
            panelPromptDetailsTitle.Size = new Size(845, 83);
            panelPromptDetailsTitle.TabIndex = 10;
            // 
            // labelPromptDetails
            // 
            labelPromptDetails.AutoSize = true;
            labelPromptDetails.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelPromptDetails.ForeColor = Color.FromArgb(230, 230, 230);
            labelPromptDetails.Location = new Point(2, 24);
            labelPromptDetails.Margin = new Padding(2, 0, 2, 0);
            labelPromptDetails.Name = "labelPromptDetails";
            labelPromptDetails.Size = new Size(185, 32);
            labelPromptDetails.TabIndex = 9;
            labelPromptDetails.Text = "Prompt Details";
            // 
            // PromptDesigner
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1289, 781);
            Controls.Add(splitContainerPromptRepo);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "PromptDesigner";
            Text = "PromptRepository";
            splitContainerPromptRepo.Panel1.ResumeLayout(false);
            splitContainerPromptRepo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerPromptRepo).EndInit();
            splitContainerPromptRepo.ResumeLayout(false);
            panelPromptList.ResumeLayout(false);
            panelPromptList.PerformLayout();
            panelPromptText.ResumeLayout(false);
            groupBoxPromptText.ResumeLayout(false);
            groupBoxPromptText.PerformLayout();
            panelPromptDetailsBottomControls.ResumeLayout(false);
            panelPromptDetailsTopControls.ResumeLayout(false);
            groupBoxPromptName.ResumeLayout(false);
            groupBoxPromptName.PerformLayout();
            panelPromptDetailsTitle.ResumeLayout(false);
            panelPromptDetailsTitle.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private SplitContainer splitContainerPromptRepo;
        private Button buttonDeletePrompt;
        private Button buttonAddPrompt;
        private Label labelPromptsList;
        private Label labelPromptDetails;
        private Panel panelPromptList;
        private ListBox listBoxPrompts;
        private Panel panelPromptDetailsTitle;
        private Panel panelPromptDetailsBottomControls;
        private Panel panelPromptDetailsTopControls;
        private TextBox textBoxPromptName;
        private TextBox textBoxPrompt;
        private Button buttonSavePrompt;
        private Panel panelPromptText;
        private GroupBox groupBoxPromptName;
        private GroupBox groupBoxPromptText;
    }
}
namespace CanvasCaptureUI.Classes
{
    internal class ImageCropper(PictureBox pictureBox) : IDisposable
    {
        private readonly PictureBox DisplayBox = pictureBox;
        private int XDown;
        private int YDown;
        private int XUp;
        private int YUp;

        private bool UserConfirmed = false;
        private Rectangle AreaToCrop;

        public async Task<Rectangle> GetCanvasArea()
        {
            // Initialize Events
            DisplayBox.MouseDown += DisplayBox_MouseDown;
            DisplayBox.MouseUp += DisplayBox_MouseUp;
            DisplayBox.MouseClick += DisplayBox_Click;

            while (!UserConfirmed)
            {
                await Task.Delay(200);
            }

            // Remove Events
            DisplayBox.MouseDown -= DisplayBox_MouseDown;
            DisplayBox.MouseUp -= DisplayBox_MouseUp;
            DisplayBox.MouseClick -= DisplayBox_Click;

            return AreaToCrop;
        }

        private void DisplayBox_Click(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Please confirm canvas area", "Confirm Canvas Area", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) UserConfirmed = true;
        }

        private void DisplayBox_MouseDown(object? sender, MouseEventArgs e)
        {
            DisplayBox.Invalidate();

            if (e.Button != MouseButtons.Left) return;

            XDown = e.X;
            YDown = e.Y;
        }
        
        private void DisplayBox_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            XUp = e.X;
            YUp = e.Y;

            Rectangle rec = new(XDown, YDown, Math.Abs(XUp - XDown), Math.Abs(YUp - YDown));

            using (Pen pen = new(Color.DarkRed, 4))
            {
                DisplayBox.CreateGraphics().DrawRectangle(pen, rec);
            }
            
            AreaToCrop = rec;
        }

        public void Dispose()
        {
            DisplayBox.MouseDown -= DisplayBox_MouseDown;
            DisplayBox.MouseUp -= DisplayBox_MouseUp;
            DisplayBox.MouseClick -= DisplayBox_Click;
        }
    }
}

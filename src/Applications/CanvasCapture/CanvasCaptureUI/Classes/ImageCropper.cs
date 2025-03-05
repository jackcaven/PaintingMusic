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
        private bool IsDrawing = false;

        public async Task<Rectangle> GetCanvasArea()
        {
            // Initialize Events
            DisplayBox.MouseDown += DisplayBox_MouseDown;
            DisplayBox.MouseUp += DisplayBox_MouseUp;
            DisplayBox.MouseClick += DisplayBox_Click;
            DisplayBox.MouseMove += DisplayBox_MouseMove;
            DisplayBox.Paint += DisplayBox_Paint;

            while (!UserConfirmed)
            {
                await Task.Delay(200);
            }

            // Remove Events
            DisplayBox.MouseDown -= DisplayBox_MouseDown;
            DisplayBox.MouseUp -= DisplayBox_MouseUp;
            DisplayBox.MouseClick -= DisplayBox_Click;
            DisplayBox.MouseMove -= DisplayBox_MouseMove;
            DisplayBox.Paint -= DisplayBox_Paint;

            return AreaToCrop;
        }

        private void DisplayBox_Paint(object? sender, PaintEventArgs e)
        {
            if (IsDrawing || AreaToCrop != Rectangle.Empty)
            {
                Rectangle rec = new(XDown, YDown, Math.Abs(XUp - XDown), Math.Abs(YUp - YDown));

                using Pen pen = new(Color.DarkRed, 4);
                e.Graphics.DrawRectangle(pen, rec);
            }
        }

        private void DisplayBox_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!IsDrawing)
                return;

            XUp = e.X;
            YUp = e.Y;

            DisplayBox.Invalidate();
        }

        private void DisplayBox_Click(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            DialogResult result = MessageBox.Show("Please confirm canvas area", "Confirm Canvas Area", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserConfirmed = true;
                XUp = XDown = YUp = YDown = 0;
                DisplayBox.Invalidate();
            }
        }

        private void DisplayBox_MouseDown(object? sender, MouseEventArgs e)
        {
            DisplayBox.Invalidate();

            if (e.Button != MouseButtons.Left) return;

            XDown = e.X;
            YDown = e.Y;

            IsDrawing = true;
        }

        private void DisplayBox_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            XDown = Math.Clamp(XDown, 0, DisplayBox.Image!.Width);
            YDown = Math.Clamp(YDown, 0, DisplayBox.Image.Height);
            XUp = Math.Clamp(e.X, 0, DisplayBox.Image.Width);
            YUp = Math.Clamp(e.Y, 0, DisplayBox.Image.Height);

            AreaToCrop = new(XDown, YDown, Math.Abs(XUp - XDown), Math.Abs(YUp - YDown));

            DisplayBox.Invalidate();
            IsDrawing = false;
        }

        public void Dispose()
        {
            DisplayBox.MouseDown -= DisplayBox_MouseDown;
            DisplayBox.MouseUp -= DisplayBox_MouseUp;
            DisplayBox.MouseClick -= DisplayBox_Click;
            DisplayBox.MouseMove -= DisplayBox_MouseMove;
            DisplayBox.Paint -= DisplayBox_Paint;
        }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace Editor
{
    public class BaseButton : Button
    {
        public TextBox BoxX;
        public TextBox BoxY;
        public Point LocPoint;
        private Point DownPoint;
        private bool IsDragMode;
        private Canvas _canvas = new Canvas();
        private ToolTip _tooltip = new ToolTip();

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            DownPoint = mevent.Location;
            IsDragMode = true;
            base.OnMouseDown(mevent);    
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            IsDragMode = false;
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (!IsDragMode)
                return;

            Point point = new Point(mevent.Location.X - DownPoint.X, mevent.Location.Y - DownPoint.Y);
            
            if (Program.MainForm.GetSize().Width > Location.X + point.X && Program.MainForm.GetSize().Height > Location.Y + point.Y)
            {
                if (Location.X + point.X > 0 && Location.Y + point.Y > 0)
                {
                    int x = Location.X + point.X;
                    int y = Location.Y + point.Y;
                    Location = new Point(x, y);
                    LocPoint = Location;
                    BoxX.Text = (Location.X + 4).ToString();
                    BoxY.Text = (Location.Y + 4).ToString();
                }
            }
            _canvas.ClearField(Program.MainForm.GetPictureBox());
            _tooltip.RemoveAll();
            Program.MainForm.DrawFigures();
        }
    }
}

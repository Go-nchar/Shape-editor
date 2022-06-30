using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Editor
{
    [Serializable]
    public class Circle : Figure
    {
        private Rectangle _rect = new Rectangle();
        private Point center;

        public override void Init(List<Point> points, int startX, PictureBox pictureBox, Panel panel, int? startY,
            Action<BaseButton> mouseEnter, Action<BaseButton> mouseUp, Action<object, KeyPressEventArgs, TextBox> keyPress,
                Action<BaseButton> textChanged)
        {
            base.Init(points, startX, pictureBox, panel, startY, mouseEnter, mouseUp, keyPress, textChanged);
            center = PointButtons.First().Location;
        }

        public override void Draw(Graphics g, PictureBox pictureBox)
        {
            Pen pen = new Pen(Color.Black, 2f);

            if (PointButtons.Count < 2)
            return;

            Point newCenter = PointButtons.First().Location;
            int deffX = newCenter.X - center.X;
            int deffY = newCenter.Y - center.Y;
            center = newCenter;

            PointButtons.Last().Location = new Point(
                PointButtons.Last().Location.X + deffX, 
                PointButtons.Last().Location.Y + deffY);

            int x = center.X - PointButtons.Last().Location.X;
            int y = center.Y - PointButtons.Last().Location.Y;
            int radius = (int) Math.Sqrt(x * x + y * y);

            _rect.Width = radius * 2;
            _rect.Height = radius * 2;
            _rect.X = center.X + 4 - radius;
            _rect.Y = center.Y + 4 - radius;

            foreach (var b in PointButtons)
            {
                pictureBox.Controls.Add(b);
            }

            g.DrawEllipse(pen, _rect);
            g.Dispose();
        }
    }
}

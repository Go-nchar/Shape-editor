using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Editor
{
    [Serializable]
    public class Rect : Figure
    {
        private Rectangle _rect = new Rectangle();
        private Point _center;

        public override void Init(List<Point> points, int startX, PictureBox pictureBox, Panel panel, int? startY,
            Action<BaseButton> mouseEnter, Action<object, KeyPressEventArgs, TextBox> keyPress,
                Action<BaseButton> textChanged)
        {
            base.Init(points, startX, pictureBox, panel, startY, mouseEnter, keyPress, textChanged);
            _center = PointButtons.First().Location;
        }

        public override void Draw(Graphics g, PictureBox pictureBox)
        {
            Pen pen = new Pen(Color.Black, 2f);

            if (PointButtons.Count < 2)
                return;

            Point newCenter = PointButtons.First().Location;
            int diffX = newCenter.X - _center.X;
            int diffY = newCenter.Y - _center.Y;
            _center = newCenter;

            PointButtons.Last().Location = new Point(
                PointButtons.Last().Location.X + diffX, 
                PointButtons.Last().Location.Y + diffY);

            int width = Math.Abs(_center.X - PointButtons.Last().Location.X) * 2;
            int height = Math.Abs(_center.Y - PointButtons.Last().Location.Y) * 2;

            _rect.Width = width;
            _rect.Height = height;
            _rect.X = _center.X + 4 - width / 2;
            _rect.Y = _center.Y + 4 - height / 2;

            foreach (var b in PointButtons)
                pictureBox.Controls.Add(b);

            g.DrawRectangle(pen, _rect);
            g.Dispose();
        }
    }
}

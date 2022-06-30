using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Editor
{
    [Serializable]
    public class Pentagon : Figure
    {
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
                PointButtons.Last().Location.X + deffX, PointButtons.Last().Location.Y + deffY);

            int x = center.X - PointButtons.Last().Location.X;
            int y = center.Y - PointButtons.Last().Location.Y;
            int radius = (int) Math.Sqrt(x * x + y * y);

            double angle = 2.0 * Math.PI / 5.0;
            IEnumerable<PointF> source = Enumerable.Range(0, 5).Select(i => PointF.Add((PointF) center, 
                new SizeF(
                (float) (Math.Sin( i * angle) * radius + 4.0),
                (float) (Math.Cos(i * angle) * radius + 4.0))));

            foreach (var b in PointButtons)
                pictureBox.Controls.Add( b);

            g.DrawPolygon(pen, source.ToArray());
            g.Dispose();
        }
    }
}

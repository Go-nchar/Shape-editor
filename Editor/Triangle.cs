using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Editor
{
    [Serializable]
    public class Triangle : Figure
    {
        public override void Draw(Graphics g, PictureBox pictureBox)
        {
            Pen pen = new Pen(Color.Black, 2f);
            List<Point> points = new List<Point>();

            foreach (var b in PointButtons)
            {
                List<Point> newPoints = points;

                newPoints.Add(new Point(b.Location.X + 4, b.Location.Y + 4));
                pictureBox.Controls.Add(b);
            }

            if (points.Count < 3)
                return;

            g.DrawPolygon(pen, points.ToArray());
            g.Dispose();
        }
    }
}

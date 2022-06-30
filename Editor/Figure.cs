using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Editor
{
    [Serializable]
    public class Figure
    {
        [JsonIgnore]
        public List<BaseButton> PointButtons = new List<BaseButton>();
        public List<Point> Points = new List<Point>();

        public void AddPointButton(BaseButton button)
        {
            if (PointButtons.Contains(button))
            return;
            PointButtons.Add(button);
        }

        public virtual void Init(List<Point> points, int startX, PictureBox pictureBox, Panel panel, int? startY,
            Action<BaseButton> mouseEnter, Action<BaseButton> mouseUp, Action<object, KeyPressEventArgs, TextBox> keyPress,
                Action<BaseButton> textChanged)
        {
            Points = points;
            List<TextBox> TextBoxes = new List<TextBox>();

            for (int i = 0; i < points.Count; ++i)
            {
                BaseButton button = new BaseButton();
                button.Name = "button";
                button.Size = new Size(8, 8);
                button.BackColor = Color.White;
                button.FlatAppearance.BorderSize = 0;
                button.FlatStyle = FlatStyle.Popup;
                button.Location = points[i];

                AddPointButton(button);
                pictureBox.Controls.Add(button);
                button.MouseUp += (a, s) => mouseUp(button);
                button.MouseEnter += (a, s) => mouseEnter(button);

                TextBox textBox1 = TextBoxes.LastOrDefault();
                Point point;
                int? diffY;

                if (textBox1 == null)
                {
                    diffY = new int?();
                }
                else
                {
                    point = textBox1.Location;
                    diffY = new int?(point.Y);
                }

                if (!diffY.HasValue)
                {
                    if (!startY.HasValue)
                    {
                        diffY = new int?(30);
                    }
                    else
                    {
                        diffY = startY.HasValue ? new int?(startY.GetValueOrDefault() + 30) : new int?();
                    }
                }
                else
                {
                    diffY = diffY.HasValue ? new int?(diffY.GetValueOrDefault() + 22) : new int?();
                }

                TextBox boxX = InitTextBox(new Point(startX, diffY.Value), new Size(40, 20));
                button.BoxX = boxX;
                boxX.KeyPress += (s, a) => keyPress(s, a, boxX);
                boxX.TextChanged += (s, a) => textChanged(button);
                TextBox textBox2 = boxX;
                point = points[i];
                int num = point.X;
                string str1 = num.ToString();
                textBox2.Text = str1;

                TextBox boxY = InitTextBox(new Point(startX + 45, diffY.Value), new Size(40, 20));
                button.BoxY = boxY;
                boxY.KeyPress += (s, a) => keyPress(s, a, boxY);
                boxY.TextChanged += (s, a) => textChanged(button);
                TextBox textBox3 = boxY;
                point = points[i];
                num = point.Y;
                string str2 = num.ToString();
                textBox3.Text = str2;

                TextBoxes.Add(boxX);
                TextBoxes.Add(boxY);
                panel.Controls.Add(boxX);
                panel.Controls.Add(boxY);
            }
        }

        protected TextBox InitTextBox(Point location, Size size)
        {
            TextBox textBox = new TextBox();
            textBox.Dock = DockStyle.None;
            textBox.Location = location;
            textBox.Multiline = true;
            textBox.Size = size;
            return textBox;
        }

        public virtual void Draw(Graphics g, PictureBox pictureBox)
        {

        }
    }
}

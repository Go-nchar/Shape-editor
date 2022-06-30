using System.Drawing;
using System.Windows.Forms;

namespace Editor
{
    public class Canvas
    {
        public static void SetSize(PictureBox pictureBox, int width, int height)
        {
            pictureBox.Size = new Size(pictureBox.Width = width, pictureBox.Height = height);
            pictureBox.BackColor = Color.Turquoise;
        }

        public Graphics Field(PictureBox pictureBox) => pictureBox.CreateGraphics();

        public void ClearField(PictureBox pictureBox)
        {
            pictureBox.Controls.Clear();
            pictureBox.CreateGraphics().Clear(Color.Turquoise);
        }
    }
}

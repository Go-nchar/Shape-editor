using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    partial class Form1 : Form
    {

        private Button _triangleButton;
        private PictureBox _pictureBox;
        private Button _clearButton;
        private Panel _panel;
        private Button _circleButton;
        private Button _pentagonButton;
        private Button _rectangleButton;
        private Button _saveButton;
        private Button _loadButton;

        private SaveFileDialog _saveFileDialog;
        private OpenFileDialog _openFileDialog;

        private TextBox _rectCountBox;
        private TextBox _pentagonCountBox;
        private TextBox _circleCountBox;
        private TextBox _triangleCountBox;

        private void InitializeComponent()
        {
            _triangleButton = new Button();
            _pictureBox = new PictureBox();
            _clearButton = new Button();
            _panel = new Panel();
            _rectCountBox = new TextBox();
            _pentagonCountBox = new TextBox();
            _circleCountBox = new TextBox();
            _triangleCountBox = new TextBox();
            _circleButton = new Button();
            _pentagonButton = new Button();
            _rectangleButton = new Button();
            _saveButton = new Button();
            _loadButton = new Button();

            _saveFileDialog = new SaveFileDialog();
            _openFileDialog = new OpenFileDialog();

            ((ISupportInitialize)_pictureBox).BeginInit();
            _panel.SuspendLayout();
            SuspendLayout();

            _pictureBox.Location = new Point(100, 60);
            _pictureBox.Name = "pictureBox1";
            _pictureBox.Size = new Size(660, 725);
            _pictureBox.TabIndex = 0;
            _pictureBox.TabStop = false;
            _pictureBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;

            _panel.AutoScroll = true;
            _panel.BackColor = SystemColors.Window;
            _panel.Controls.Add((Control)_rectCountBox);
            _panel.Controls.Add((Control)_pentagonCountBox);
            _panel.Controls.Add((Control)_circleCountBox);
            _panel.Controls.Add((Control)_triangleCountBox);
            _panel.Dock = DockStyle.Right;
            _panel.Location = new Point(784, 12);
            _panel.Name = "panel1";
            _panel.Size = new Size(400, 773);
            _panel.TabIndex = 1;
            _panel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            _triangleCountBox.BackColor = SystemColors.Window;
            _triangleCountBox.BorderStyle = BorderStyle.None;
            _triangleCountBox.Location = new Point(5, 5);
            _triangleCountBox.Multiline = true;
            _triangleCountBox.Name = "TriangleCountBox";
            _triangleCountBox.ReadOnly = true;
            _triangleCountBox.Size = new Size(95, 20);
            _triangleCountBox.TabIndex = 0;
            _triangleCountBox.Text = "Triangles:";

            _circleCountBox.BackColor = SystemColors.Window;
            _circleCountBox.BorderStyle = BorderStyle.None;
            _circleCountBox.Location = new Point(103, 5);
            _circleCountBox.Multiline = true;
            _circleCountBox.Name = "CircleCountBox";
            _circleCountBox.ReadOnly = true;
            _circleCountBox.Size = new Size(95, 20);
            _circleCountBox.TabIndex = 2;
            _circleCountBox.Text = "Circles:";

            _pentagonCountBox.BackColor = SystemColors.Window;
            _pentagonCountBox.BorderStyle = BorderStyle.None;
            _pentagonCountBox.Location = new Point(201, 5);
            _pentagonCountBox.Multiline = true;
            _pentagonCountBox.Name = "PentagonCountBox";
            _pentagonCountBox.ReadOnly = true;
            _pentagonCountBox.Size = new Size(95, 20);
            _pentagonCountBox.TabIndex = 3;
            _pentagonCountBox.Text = "Pentagons:";

            _rectCountBox.BackColor = SystemColors.Window;
            _rectCountBox.BorderStyle = BorderStyle.None;
            _rectCountBox.Location = new Point(299, 5);
            _rectCountBox.Multiline = true;
            _rectCountBox.Name = "RectCountBox";
            _rectCountBox.ReadOnly = true;
            _rectCountBox.Size = new Size(95, 20);
            _rectCountBox.TabIndex = 4;
            _rectCountBox.Text = "Rectangles:";

            _triangleButton.Location = new Point(100, 12);
            _triangleButton.Name = "Triangle";
            _triangleButton.Size = new Size(100, 30);
            _triangleButton.TabIndex = 5;
            _triangleButton.Text = "Triangle";
            _triangleButton.UseVisualStyleBackColor = true;
            _triangleButton.Click += new EventHandler(TriangleButtonClick);

            _circleButton.Location = new Point(205, 12);
            _circleButton.Name = "Circle";
            _circleButton.Size = new Size(100, 30);
            _circleButton.TabIndex = 6;
            _circleButton.Text = "Circle";
            _circleButton.UseVisualStyleBackColor = true;
            _circleButton.Click += new EventHandler(CircleButtonClick);

            _pentagonButton.Location = new Point(310, 12);
            _pentagonButton.Name = "Pentagon";
            _pentagonButton.Size = new Size(100, 30);
            _pentagonButton.TabIndex = 7;
            _pentagonButton.Text = "Pentagon";
            _pentagonButton.UseVisualStyleBackColor = true;
            _pentagonButton.Click += new EventHandler(PentagonButtonClick);

            _rectangleButton.Location = new Point(415, 12);
            _rectangleButton.Name = "Rectangle";
            _rectangleButton.Size = new Size(100, 30);
            _rectangleButton.TabIndex = 8;
            _rectangleButton.Text = "Rectangle";
            _rectangleButton.UseVisualStyleBackColor = true;
            _rectangleButton.Click += new EventHandler(RectButtonClick);

            _clearButton.Location = new Point(12, 12);
            _clearButton.Name = "Erase";
            _clearButton.Size = new Size(80, 30);
            _clearButton.TabIndex = 9;
            _clearButton.Text = "Erase";
            _clearButton.UseVisualStyleBackColor = true;

            _saveButton.Location = new Point(12, 60);
            _saveButton.Name = "Save";
            _saveButton.Size = new Size(80, 30);
            _saveButton.TabIndex = 11;
            _saveButton.Text = "Save";
            _saveButton.UseVisualStyleBackColor = true;
            _saveButton.Click += new EventHandler(OnSaveFigures);

            _loadButton.Location = new Point(12, 95);
            _loadButton.Name = "Load";
            _loadButton.Size = new Size(80, 30);
            _loadButton.TabIndex = 10;
            _loadButton.Text = "Load";
            _loadButton.UseVisualStyleBackColor = true;
            _loadButton.Click += new EventHandler(OnLoadFigures);

            _openFileDialog.FileName = "openFileDialog1";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            MinimumSize = new Size(1020, 650);

            Controls.Add((Control)_panel);

            Controls.Add((Control)_clearButton);
            Controls.Add((Control)_saveButton);
            Controls.Add((Control)_loadButton);
            
            Controls.Add((Control)_rectangleButton);
            Controls.Add((Control)_pentagonButton);
            Controls.Add((Control)_circleButton);
            Controls.Add((Control)_triangleButton);

            Controls.Add((Control)_pictureBox);
            

            Name = nameof(Form1);
            Text = nameof(Form1);
            ((ISupportInitialize)_pictureBox).EndInit();
            _panel.ResumeLayout(false);
            _panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

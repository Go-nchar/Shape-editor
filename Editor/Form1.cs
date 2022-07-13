using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form1 : Form
    {
        private static FiguresData _figuresData = new FiguresData();
        private Random _random = new Random();

        private int _triangleCount = 0;
        private int _circleCount = 0;
        private int _pentagonCount = 0;
        private int _rectCount = 0;

        private ToolTip _tooltip = new ToolTip();
        private Canvas _canvas = new Canvas();

        public Form1()
        {
            InitializeComponent();

            Canvas.SetSize(_pictureBox);
            _clearButton.Click += (s, e) => ClearButtonClick();

            _openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            _saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            DrawFigures();
        }

        public Size GetSize()
        {
            return _pictureBox.Size;
        }

        private void OnLoadFigures(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            string fileName = _openFileDialog.FileName;
            ClearButtonClick();

            _figuresData = Storage.Load<FiguresData>(fileName);
            InitFigures();
        }

        private void OnSaveFigures(object sender, EventArgs e)
        {
            if (_saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            Storage.Save(_saveFileDialog.FileName, _figuresData);
            MessageBox.Show("File saved");
        }

        private void InitFigures()
        {
            foreach (Figure figure in _figuresData.Figures)
            {
                (int startX, int? startY) = GetSrartXY(figure);

                figure.Init(figure.Points, startX, _pictureBox, _panel, startY,
                    new Action<BaseButton>(OnButtonMouseEnter), 
                    new Action<object, KeyPressEventArgs, TextBox>(TextBoxKeyPress), 
                    new Action<BaseButton>(FigureTextChanged));

                figure.Draw(_canvas.Field(_pictureBox), _pictureBox);
                UpdateCount(figure);
            }
        }

        public void UpdateCount(Figure figure)
        {
            switch (figure)
            {
                case Triangle _:
                    _triangleCount++;
                    _triangleCountBox.Text = "Triangles:" + _triangleCount.ToString();
                    break;
                case Circle _:
                    _circleCount++;
                    _circleCountBox.Text = "Circles:" + _circleCount.ToString();
                    break;
                case Pentagon _:
                    _pentagonCount++;
                    _pentagonCountBox.Text = "Pentagons:" + _pentagonCount.ToString();
                    break;
                case Rect _:
                    _rectCount++;
                    _rectCountBox.Text = "Rectangles:" + _rectCount.ToString();
                    break;
            }
        }

        public void DrawFigures()
        {
            foreach (Figure figure in _figuresData.Figures)
            {
                figure.Draw(_canvas.Field(_pictureBox), _pictureBox);

                //if (figure != null)
                //{
                //    foreach (BaseButton button in figure.PointButtons)
                //    {
                //        button.BoxX.Text = (button.Location.X + 4).ToString();
                //        button.BoxY.Text = (button.Location.Y + 4).ToString();
                //    }
                //}
            }
        }

        private void TriangleButtonClick(object sender, EventArgs e)
        {
            Triangle triangle = new Triangle();
            Point[] points = new Point[3];

            for (int i = 0; i < 3; ++i)
            {
                points[i].X = _random.Next(0, _pictureBox.Size.Width);
                points[i].Y = _random.Next(0, _pictureBox.Size.Height);
            }
            List<TextBox> textBoxList = new List<TextBox>();
            Figure figure = _figuresData.Figures.FindAll(f => f is Triangle).LastOrDefault();
            int? startY = figure != null ? figure.PointButtons.LastOrDefault()?.BoxY.Location.Y : new int?();

            _figuresData.Figures.Add(triangle);

            triangle.Init(points.ToList(), 5, _pictureBox, _panel, startY,
                new Action<BaseButton>(OnButtonMouseEnter), 
                new Action<object, KeyPressEventArgs, TextBox>(TextBoxKeyPress), new Action<BaseButton>(FigureTextChanged));

            triangle.Draw(_canvas.Field(_pictureBox), _pictureBox);
            UpdateCount(triangle);
        }

        private void CircleButtonClick(object sender, EventArgs e)
        {
            Circle circle = new Circle();
            Point[] points = new Point[2];

            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                {
                    points[i].X = _random.Next(0, _pictureBox.Size.Width);
                    points[i].Y = _random.Next(0, _pictureBox.Size.Height);
                }
                else
                {
                    List<int> radius = new List<int>();
                    radius.Add(Math.Abs(points[0].X - _pictureBox.Size.Width));
                    radius.Add(Math.Abs(points[0].Y - _pictureBox.Size.Height));
                    radius.Add(points[0].X);
                    radius.Add(points[0].Y);

                    points[i].X = _random.Next(points[0].X - radius.Min(), points[0].X + radius.Min());
                    points[i].Y = _random.Next(points[0].Y - radius.Min(), points[0].Y + radius.Min());
                }
            }
            List<TextBox> textBoxList = new List<TextBox>();
            Figure figure = _figuresData.Figures.FindAll(f => f is Circle).LastOrDefault();
            int? startY = figure != null ? figure.PointButtons.LastOrDefault()?.BoxY.Location.Y : new int?();

            _figuresData.Figures.Add(circle);

            circle.Init(points.ToList(), 103, _pictureBox, _panel, startY,
                new Action<BaseButton>(OnButtonMouseEnter), 
                new Action<object, KeyPressEventArgs, TextBox>(TextBoxKeyPress), new Action<BaseButton>(FigureTextChanged));

            circle.Draw(_canvas.Field(_pictureBox), _pictureBox);
            UpdateCount(circle);
        }

        private (int, int?) GetSrartXY(Figure figure)
        {
            List<TextBox> textBoxList = new List<TextBox>();
            List<Figure> figures = _figuresData.Figures.FindAll((f => f.GetType() == figure.GetType()));
            int num1 = figures.IndexOf(figure);
            int? nullable = new int?();

            if (num1 > 0)
                nullable = figures[num1 - 1].PointButtons.LastOrDefault()?.BoxY.Location.Y;

            int num2 = 0;
            switch (figure)
            {
                case Triangle _:
                    num2 = 5;
                    break;
                case Circle _:
                    num2 = 103;
                    break;
                case Pentagon _:
                    num2 = 201;
                    break;
                case Rect _:
                    num2 = 299;
                    break;
            }
            return (num2, nullable);
        }

        private void PentagonButtonClick(object sender, EventArgs e)
        {
            Pentagon pentagon = new Pentagon();
            Point[] points = new Point[2];

            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                {
                    points[i].X = _random.Next(0, _pictureBox.Size.Width);
                    points[i].Y = _random.Next(0, _pictureBox.Size.Height);
                }
                else
                {
                    List<int> radius = new List<int>();
                    radius.Add(Math.Abs(points[0].X - _pictureBox.Size.Width));
                    radius.Add(Math.Abs(points[0].Y - _pictureBox.Size.Height));
                    radius.Add(points[0].X);
                    radius.Add(points[0].Y);

                    points[i].X = _random.Next(points[0].X - radius.Min(), points[0].X + radius.Min());
                    points[i].Y = _random.Next(points[0].Y - radius.Min(), points[0].Y + radius.Min());
                }
            }

            List<TextBox> textBoxList = new List<TextBox>();
            Figure figure = _figuresData.Figures.FindAll(f => f is Pentagon).LastOrDefault();
            int? startY = figure != null ? figure.PointButtons.LastOrDefault()?.BoxY.Location.Y : new int?();

            _figuresData.Figures.Add(pentagon);

            pentagon.Init(points.ToList(), 201, _pictureBox, _panel, startY,
                new Action<BaseButton>(OnButtonMouseEnter), 
                new Action<object, KeyPressEventArgs, TextBox>(TextBoxKeyPress), new Action<BaseButton>(FigureTextChanged));

            pentagon.Draw(_canvas.Field(_pictureBox), _pictureBox);
            UpdateCount(pentagon);
        }

        private void RectButtonClick(object sender, EventArgs e)
        {
            Rect rect = new Rect();
            Point[] points = new Point[2];

            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                {
                    points[i].X = _random.Next(0, _pictureBox.Size.Width);
                    points[i].Y = _random.Next(0, _pictureBox.Size.Height);
                }
                else
                {
                    List<int> radius = new List<int>();
                    radius.Add(Math.Abs(points[0].X - _pictureBox.Size.Width));
                    radius.Add(Math.Abs(points[0].Y - _pictureBox.Size.Height));
                    radius.Add(points[0].X);
                    radius.Add(points[0].Y);

                    points[i].X = _random.Next(points[0].X - radius.Min(), points[0].X + radius.Min());
                    points[i].Y = _random.Next(points[0].Y - radius.Min(), points[0].Y + radius.Min());
                }
            }

            List<TextBox> textBoxList = new List<TextBox>();
            Figure figure = _figuresData.Figures.FindAll(f => f is Rect).LastOrDefault();
            int? startY = figure != null ? figure.PointButtons.LastOrDefault()?.BoxY.Location.Y : new int?();

            _figuresData.Figures.Add(rect);

            rect.Init(points.ToList(), 299, _pictureBox, _panel, startY,
                new Action<BaseButton>(OnButtonMouseEnter), 
                new Action<object, KeyPressEventArgs, TextBox>(TextBoxKeyPress), new Action<BaseButton>(FigureTextChanged));

            rect.Draw(_canvas.Field(_pictureBox), _pictureBox);
            UpdateCount(rect);
        }

        private void ClearButtonClick()
        {
            _canvas.ClearField(_pictureBox);

            foreach (Figure figure in _figuresData.Figures)
            {
                foreach (BaseButton pointButton in figure.PointButtons)
                {
                    _panel.Controls.Remove(pointButton.BoxX);
                    _panel.Controls.Remove(pointButton.BoxY);
                    _pictureBox.Controls.Remove(pointButton);
                }
            }

            _triangleCountBox.Text = "Triangles:";
            _circleCountBox.Text = "Circles:";
            _pentagonCountBox.Text = "Pentagons:";
            _rectCountBox.Text = "Rectangles:";

            _triangleCount = 0;
            _circleCount = 0;
            _pentagonCount = 0;
            _rectCount = 0;

            _figuresData.Figures.Clear();
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs e, TextBox box)
        {
            char keyChar = e.KeyChar;
            box.MaxLength = 4;

            if (char.IsDigit(keyChar) || keyChar == '\b')
                return;

            e.Handled = true;
        }

        private void OnButtonMouseEnter(BaseButton button)
        {
            ToolTip tooltip = _tooltip;
            Point point = new Point(button.Location.X + 4, button.Location.Y + 4);
            tooltip.SetToolTip(button, point.ToString());
        }

        //public void OnButtonMouseUp(BaseButton button)
        //{
        //    button.Location = button.LocPoint;
        //    button.BoxX.Text = (button.LocPoint.X + 4).ToString();
        //    button.BoxY.Text = (button.LocPoint.Y + 4).ToString();
            
        //    _canvas.ClearField(_pictureBox);
        //    _tooltip.RemoveAll();
        //    DrawFigures();
        //}

        public PictureBox GetPictureBox()
        {
            return _pictureBox;
        }

        public void FigureTextChanged(BaseButton button)
        {
            int x = 0;
            if (button.BoxX != null && button.BoxX.Text.Length != 0)
            {
                if (int.Parse(button.BoxX.Text) < _pictureBox.Size.Width)
                {
                    x = int.Parse(button.BoxX.Text);
                }
                else
                {
                    x = int.Parse(button.BoxX.Text = _pictureBox.Size.Width.ToString());
                }
            }

            int y = 0;
            if (button.BoxY != null && button.BoxY.Text.Length != 0)
            {
                if (int.Parse(button.BoxY.Text) < _pictureBox.Size.Height)
                {
                    y = int.Parse(button.BoxY.Text);
                }
                else
                {
                    y = int.Parse(button.BoxY.Text = _pictureBox.Size.Height.ToString());
                }
            }

            button.Location = new Point(x - 4, y - 4);
            _tooltip.RemoveAll();
            OnButtonMouseEnter(button);
            _canvas.ClearField(_pictureBox);
            DrawFigures();
        }
    }
}

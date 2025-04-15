using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CcontrolsZaxx.CropBox
{
    public partial class CropBox : UserControl, INotifyPropertyChanged
    {
        public CropBox()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        Point m_start;
        Vector m_startOffset;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(this);
            m_startOffset = new Vector(tt.X, tt.Y);
            grid.CaptureMouse();
        }

        private void Grid_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (grid.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(this), m_start);

                tt.X = m_startOffset.X + offset.X;
                tt.Y = m_startOffset.Y + offset.Y;

                if (tt.Y < 0 && tt.Y < -CheckSize.ActualHeight / 2 + grid.Height / 2) { tt.Y = -(CheckSize.ActualHeight / 2) + grid.Height / 2; }
                if (tt.Y > 0 && tt.Y > CheckSize.ActualHeight / 2 - grid.Height / 2) { tt.Y = (CheckSize.ActualHeight / 2) - grid.Height / 2; }

                if (tt.X < 0 && tt.X < -CheckSize.ActualWidth / 2 + grid.Width / 2) { tt.X = -(CheckSize.ActualWidth / 2) + grid.Width / 2; }
                if (tt.X > 0 && tt.X > CheckSize.ActualWidth / 2 - grid.Width / 2) { tt.X = (CheckSize.ActualWidth / 2) - grid.Width / 2; }

                Info = $"x: {tt.X.ToString()} y: {tt.Y} thisHeight: {CheckSize.ActualHeight} thisWidth: {CheckSize.ActualWidth}";
                BoxMoving?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            grid.ReleaseMouseCapture();
        }

        private string Info_;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string nam = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nam));
        }

        public string Info
        {
            get { return Info_; }
            set { Info_ = value; OnPropertyChanged(); }

        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Rectangle_MouseEnter_1(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.SizeAll;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }
        private double intGridWidth;
        private double intGridHeight;
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(this);
            intGridWidth = grid.Width;
            intGridHeight = grid.Height;
            ReactToi.CaptureMouse();
        }

        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (ReactToi.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(this), m_start);

                double newWidth = intGridWidth + offset.X;
                double newHeight = intGridHeight + offset.Y;
               
                if(newWidth > 0) {  grid.Width = newWidth; }
                if(newHeight > 0) {  grid.Height = newHeight; }

                if (newWidth < 10) { grid.Width = 10; }
                if (newWidth > CheckSize.ActualWidth) { grid.Width = CheckSize.ActualWidth; }
                if (newHeight < 10) { grid.Height = 50; }
                if (newHeight > CheckSize.ActualHeight) { grid.Width = CheckSize.ActualHeight; }

                Info = $"StartDragPosMouse: x{m_start.X}, y{m_start.Y}  gridWH: {grid.Width},{grid.Height} offsetVector:x{offset.X},y{offset.Y} newGridWH:x{grid.Width + offset.X}";
            }
        }

        public event EventHandler BoxMoving;

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Debug.Print($"CurrentScaling: {Trsa.Value}");
        }

        private void ReactToi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReactToi.ReleaseMouseCapture();
        }

        public double GetTopY { get { return tt.Y; } }
        public double GetLeftX { get { return tt.X; } }
        public double GetWidth { get { return grid.ActualWidth; } }
        public double GetHeight { get { return grid.ActualHeight; } }
    }
}

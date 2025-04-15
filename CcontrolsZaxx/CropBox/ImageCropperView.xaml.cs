using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xaml;
using Point = System.Windows.Point;

namespace CcontrolsZaxx.CropBox
{

    public partial class ImageCropperView : UserControl, INotifyPropertyChanged
    {
        public ImageCropperView()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Frame_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }
        private void Frame_MouseLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
        }
        private void InFrame_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.SizeAll;
        }
        Point m_start;
        Vector m_startOffset;
        private double intGridWidth;
        private double intGridHeight;
        private void Frame_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(this);
            intGridWidth = RoiBox.Width;
            intGridHeight = RoiBox.Height;
            Frame.CaptureMouse();
        }
        private void Frame_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Frame.ReleaseMouseCapture();
        }
        private void Frame_MouseMove(object sender, MouseEventArgs e)
        {
            if (Frame.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(this), m_start);

                double newWidth = intGridWidth + offset.X;
                double newHeight = intGridHeight + offset.Y;

                if (newWidth > 0) { RoiBox.Width = newWidth; }
                if (newHeight > 0) { RoiBox.Height = newHeight; }

                if (newWidth < 10) { RoiBox.Width = 30; }
               // if (newWidth > RoiBox.ActualWidth) { RoiBox.Width = RoiBox.ActualWidth; }
                if (newHeight < 10) { RoiBox.Height = 30; }
               // if (newHeight > RoiBox.ActualHeight) { RoiBox.Width = RoiBox.ActualHeight; }

               // Info = $"StartDragPosMouse: x{m_start.X}, y{m_start.Y}  gridWH: {RoiBox.Width},{RoiBox.Height} offsetVector:x{offset.X},y{offset.Y} newGridWH:x{grid.Width + offset.X}";
            }
        }

        private void InFrame_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(this);
            m_startOffset = new Vector(tt.X, tt.Y);
            InFrame.CaptureMouse();
        }

        private void InFrame_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            InFrame.ReleaseMouseCapture();
        }

        private void InFrame_MouseMove(object sender, MouseEventArgs e)
        {
            if (InFrame.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(this), m_start);

                tt.X = m_startOffset.X + offset.X;
                tt.Y = m_startOffset.Y + offset.Y;

                //if (tt.Y < 0 && tt.Y < -CheckSize.ActualHeight / 2 + grid.Height / 2) { tt.Y = -(CheckSize.ActualHeight / 2) + grid.Height / 2; }
                //if (tt.Y > 0 && tt.Y > CheckSize.ActualHeight / 2 - grid.Height / 2) { tt.Y = (CheckSize.ActualHeight / 2) - grid.Height / 2; }

                //if (tt.X < 0 && tt.X < -CheckSize.ActualWidth / 2 + grid.Width / 2) { tt.X = -(CheckSize.ActualWidth / 2) + grid.Width / 2; }
                //if (tt.X > 0 && tt.X > CheckSize.ActualWidth / 2 - grid.Width / 2) { tt.X = (CheckSize.ActualWidth / 2) - grid.Width / 2; }

                //Info = $"x: {tt.X.ToString()} y: {tt.Y} thisHeight: {CheckSize.ActualHeight} thisWidth: {CheckSize.ActualWidth}";
                //BoxMoving?.Invoke(this, EventArgs.Empty);
            }
        }

        private BitmapImage TakeScreenShot()
        {
            try
            {
                double screenLeft = SystemParameters.VirtualScreenLeft;
                double screenTop = SystemParameters.VirtualScreenTop;
                double screenWidth = SystemParameters.VirtualScreenWidth;
                double screenHeight = SystemParameters.VirtualScreenHeight;

                //Debug.Print($"ScreenWidth:{screenWidth}, ScreenHeight:{screenHeight}, screenLeft:{screenLeft}, ScreenTop:{screenTop}");
                // using (Bitmap bmp = new Bitmap((int)screenWidth, (int)screenHeight))
                using (Bitmap bmp = new Bitmap((int)screenWidth, (int)screenHeight))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen((int)screenTop, (int)screenLeft, 0, 0, bmp.Size);
                        using (MemoryStream stm = new MemoryStream())
                        {
                            bmp.Save(stm, System.Drawing.Imaging.ImageFormat.Png);
                            stm.Position = 0;
                            BitmapImage bitmapimage = new BitmapImage();
                            bitmapimage.BeginInit();
                            bitmapimage.StreamSource = stm;
                            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapimage.EndInit();
                            return bitmapimage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private BitmapImage _ImgSource;
        public BitmapImage ImgSource
        {
            get { return _ImgSource; }
            set { _ImgSource = value; OnPropertyChanged(); }
        }
        public event Action<bool> TakingScreenShot;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TakingScreenShot?.Invoke(true);
            ImgSource = TakeScreenShot();
            TakingScreenShot?.Invoke(false);
        }
        private BitmapImage CropImage(BitmapImage Source, double top, double left, double width, double height)
        {
            Bitmap ToCrop;
            using (MemoryStream outStream = new MemoryStream()) //convert to Bitmap
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(Source));
                enc.Save(outStream);
                ToCrop = new System.Drawing.Bitmap(outStream);
            }

            Rectangle cropRect = new Rectangle((int)left, (int)top, (int)width, (int)height);

            using (Bitmap src = ToCrop) //do the cropping
            {
                using (Bitmap target = new Bitmap((int)width, (int)height))
                {
                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
                        using (MemoryStream stm = new MemoryStream())
                        {
                            target.Save(stm, System.Drawing.Imaging.ImageFormat.Png);
                            stm.Position = 0;
                            BitmapImage bitmapimage = new BitmapImage();
                            bitmapimage.BeginInit();
                            bitmapimage.StreamSource = stm;
                            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapimage.EndInit();
                            return bitmapimage;
                        }
                    }
                }
            }
        }
        public event EventHandler<BitmapImage> ImageCropped;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Point imgPoint = new Point(ImgMveXY.X, ImgMveXY.Y);
            Vector offsetVal = Point.Subtract(new Point(0,0), imgPoint);
            ImgSource = CropImage(ImgSource, offsetVal.Y + tt.Y , tt.X + offsetVal.X, RoiBox.ActualWidth,RoiBox.ActualHeight);
            //string text = $"img: x{ImgMveXY.X} y:{ImgMveXY.Y}   RoiBox: x{tt.X} y{tt.Y} offset: x{offsetVal.X} y{offsetVal.Y}"; 
            //MessageBox.Show(text);
            ImgMveXY.X = 0;
            ImgMveXY.Y = 0;
            tt.X = 0;
            tt.Y = 0;
            ImageCropped?.Invoke(sender,ImgSource);
        }

        private void Cnvss1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomInOutCanvas(0.1);
            }
            else
            {
                ZoomInOutCanvas(-0.1);
            }
        }

        private void ZoomInOutCanvas(double ScaleXY_increament)
        {
            CnvsScale.ScaleY += ScaleXY_increament;
            CnvsScale.ScaleX += ScaleXY_increament;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Imgg1.Width = ViewerFinderCanvas.ActualWidth;
            CnvsScale.ScaleY = 1; CnvsScale.ScaleX = 1;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string text = $"img: x{ImgMveXY.X} y:{ImgMveXY.Y}   RoiBox: x{tt.X} y{tt.Y}";
            MessageBox.Show(text);
        }

        private void Imgg1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(this);
            m_startOffset = new Vector(tt.X, tt.Y);
            Imgg1.CaptureMouse();
        }

        private void Imgg1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Imgg1.ReleaseMouseCapture();
        }

        private void Imgg1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Imgg1.IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(this), m_start);

                ImgMveXY.X = m_startOffset.X + offset.X;
                ImgMveXY.Y = m_startOffset.Y + offset.Y;

                
            }
        }
    }
}

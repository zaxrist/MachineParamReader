using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace CcontrolsZaxx.CropBox
{
    public partial class ImageReader : UserControl, INotifyPropertyChanged
    {
        public ImageReader()
        {
            InitializeComponent();
            this.DataContext = this;
            worker.DoWork += Worker_DoWork;
        }
        //private void Border_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    WriteStat("X: " + Mouse.GetPosition(System.Windows.Application.Current.MainWindow).X.ToString() + "  " + "Y: " + Mouse.GetPosition(System.Windows.Application.Current.MainWindow).Y.ToString());
        //}
        private BitmapImage _ImgSource;
        public BitmapImage ImgSource
        {
            get { return _ImgSource; }
            set { _ImgSource = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(5000);
            InfoText = "";
        }

        RelayCommand _TakeScreenShotCmd;
        public ICommand TakeScreenShotCmd
        {
            get
            {
                if (_TakeScreenShotCmd == null)
                {
                    _TakeScreenShotCmd = new RelayCommand(param => this.TakeScreenShot(), param => this.Enabledd);
                }
                return _TakeScreenShotCmd;
            }
        }
        private bool Enabledd = true;
        private void TakeScreenShot()
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
                            ImgSource = bitmapimage;
                        }
                    }
                }
                WriteStat("Screen Captured");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
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
        private string _InfoText;

        public string InfoText
        {
            get { return _InfoText; }
            set { _InfoText = value; OnPropertyChanged(); }
        }
        private void WriteStat(string msg)
        {
            InfoText = $"[{DateTime.Now}] {msg}";
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }
        BackgroundWorker worker = new BackgroundWorker();

        private void HeightBtnUp_Click(object sender, RoutedEventArgs e)
        {
            Imgg1.Width = WspaceGrid.ActualWidth;
        }
        private double _cvScaleX;

        public double cvScaleX
        {
            get { return _cvScaleX; }
            set { _cvScaleX = value; OnPropertyChanged(); } 
        }

        private double _cvScaleY;

        public double cvScaleY
        {
            get { return _cvScaleY; }
            set { _cvScaleY = value; OnPropertyChanged(); }
        }

        private void Cnvss1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            InfoText = $"Mouse: {e.Delta} imgW:{Imgg1.ActualHeight}, imgH:{Imgg1.ActualWidth}";
            if(e.Delta < 0 )
            {
                Imgg1.Width = Imgg1.ActualWidth - 50;
            }
            else
            {
                Imgg1.Width = Imgg1.ActualWidth + 50;
            }
        }

        private void CropBtn_Click(object sender, RoutedEventArgs e)
        {
            ImgSource = CropImage(ImgSource, CropBox1.GetTopY,CropBox1.GetLeftX,CropBox1.GetWidth,CropBox1.GetHeight);
        }

        private void CropBox1_BoxMoving(object sender, EventArgs e)
        {
            InfoText = $"Img[Top:{tt.Y} Left:{tt.X} W:{Imgg1.ActualWidth} H:{Imgg1.ActualHeight}]\n CropBox[Top:{CropBox1.GetTopY} left:{CropBox1.GetLeftX} W:{CropBox1.GetWidth} H:{CropBox1.GetHeight}]";
        }

        private void Cnvss1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("sdf");
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}

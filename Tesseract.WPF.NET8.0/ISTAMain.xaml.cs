using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tesseract.WPF.NET8._0
{
    public partial class ISTAMain : Window, INotifyPropertyChanged
    {
        public ISTAMain()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private double _XCor;
        public double XCor
        {
            get { return _XCor; }
            set { _XCor = value; OnPropertyChanged(); }
        }
        public double _YCor;
        public double YCor
        {
            get { return _YCor; }
            set { _YCor = value; OnPropertyChanged(); }
        }
        private double _Widthh;

        public double Widthh
        {
            get { return _Widthh; }
            set { _Widthh = value; OnPropertyChanged(); }
        }
        private string _Heightt;

        public string Heightt
        {
            get { return _Heightt; }
            set { _Heightt = value; OnPropertyChanged(); }
        }
        private BitmapImage _ImgSource;

        public BitmapImage ImgSource
        {
            get { return _ImgSource; }
            set { _ImgSource = value; OnPropertyChanged(); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TesserTest tt = new TesserTest();
            ImgSource = tt.ScreenShotWindow(XCor, YCor, Widthh, 10);
        }
    }
}

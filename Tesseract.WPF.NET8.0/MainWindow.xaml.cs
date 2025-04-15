using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Tesseract.WPF.NET8._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set properties for the dialog (optional)
            openFileDialog.Title = "Select a File";
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Show the dialog and get the result
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file path
                ImgPathLbl.Content = openFileDialog.FileName;
                try
                {
                    ImageFrame.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
                catch (Exception)
                {
                    return;
                }
                ResultLbl.Content = GetText(openFileDialog.FileName);
            }
            else
            {
                ImgPathLbl.Content = "";
                ImageFrame.Source = null;
            }
        }

        public string GetText(string imagePath)
        {
            using (var engine = new TesseractEngine( Environment.CurrentDirectory + "\\tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {

                        return page.GetText();
                    }
                }
            }
        }
        public string GetTextT(byte[] imgByte)
        {
            using (var engine = new TesseractEngine(Environment.CurrentDirectory + "\\tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromMemory(imgByte))
                {
                    using (var page = engine.Process(img))
                    {

                        return page.GetText();
                    }
                }
            }
        }
        private void ImageCropperView_ImageCropped(object sender, BitmapImage e)
        {
            ImageFrame.Source = e;
            ResultLbl.Content = GetTextT(ConvertBitMapImage(e));

        }
        public byte[] ConvertBitMapImage(BitmapImage img)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
                return data;
            }
        }
    }
}
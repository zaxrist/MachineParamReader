using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace Tesseract.WPF.NET8._0
{
    public class TesserTest
    {

        public BitmapImage ScreenShotWindow(double X, double Y, double Widht, double Height)
        {
            try
            {
                //double screenLeft = SystemParameters.VirtualScreenLeft;
                //double screenTop = SystemParameters.VirtualScreenTop;
                //double screenWidth = SystemParameters.VirtualScreenWidth;
                //double screenHeight = SystemParameters.VirtualScreenHeight;

                using (Bitmap bmp = new Bitmap((int)Widht, (int)Height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen((int)X, (int)Y, 0, 0, bmp.Size);
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

using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;

namespace CcontrolsZaxx.CropBox
{
    public class ScreenshotLib
    {
        public BitmapImage TakeScreenShot(double x, double y,double widthh, double heightt)
        {
            try
            {
                //double screenLeft = SystemParameters.VirtualScreenLeft;
                //double screenTop = SystemParameters.VirtualScreenTop;
                //double screenWidth = SystemParameters.VirtualScreenWidth;
                //double screenHeight = SystemParameters.VirtualScreenHeight;

                //Debug.Print($"ScreenWidth:{screenWidth}, ScreenHeight:{screenHeight}, screenLeft:{screenLeft}, ScreenTop:{screenTop}");
                // using (Bitmap bmp = new Bitmap((int)screenWidth, (int)screenHeight))
                using (Bitmap bmp = new Bitmap((int)widthh, (int)heightt))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen((int)x, (int)y, 0, 0, bmp.Size);
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

        public Bitmap TakeScreenShot1(double x, double y, double widthh, double heightt)
        {
            using (Bitmap bmp = new Bitmap((int)widthh, (int)heightt))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen((int)x, (int)y, 0, 0, bmp.Size);
                    using (MemoryStream stm = new MemoryStream())
                    {
                        bmp.Save(stm, System.Drawing.Imaging.ImageFormat.Png);
                        stm.Position = 0;
                        Bitmap bitmapimage = new Bitmap(stm);
                        return bitmapimage;
                    }
                }
            }
        }
    }
}

using System.Windows;

namespace CcontrolsZax
{

    public partial class OtherProjectWindow : Window
    {
        public OtherProjectWindow()
        {
            InitializeComponent();
        }

        private void ImageCropperView_TakingScreenShot(bool obj)
        {
            if(obj) //taking screenshot
            {
                this.Hide();
            }
            else //screensot ended
            {
                this.Show();
            }
        }
    }
}

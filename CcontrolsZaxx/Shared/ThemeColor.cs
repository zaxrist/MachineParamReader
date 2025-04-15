using System.Windows.Media;

namespace CcontrolsZaxx.Shared
{
    public static class ThemeColor
    {
        public static SolidColorBrush OutFocusTextColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b3b3b3"));
        public static SolidColorBrush InFocusTextColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
        public static SolidColorBrush MouseLeftPressedColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#808080"));
        public static SolidColorBrush CommonBackgroundColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#181818"));
    }
}

using System.Runtime.InteropServices;

namespace SystemTrayApp.From.Net8._0.StudyClass
{
    public class MouseControl
    {
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINTT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTT
        {
            public int x;
            public int y;

            public POINTT(int X, int Y)
            {
                x = X;
                y = Y;
            }

        }
        //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
    }
}

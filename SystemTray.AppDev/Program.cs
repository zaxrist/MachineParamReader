using System;
using System.Windows.Forms;

namespace SystemTray.AppDev
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Use the assembly GUID as the name of the mutex which we use to detect if an application instance is already running
            bool createdNew = false;
            string mutexName = System.Reflection.Assembly.GetExecutingAssembly().GetType().GUID.ToString();
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(false, mutexName, out createdNew))
            {
                if (!createdNew)
                {
                    // Only allow one instance
                    MessageBox.Show( $"Application {Application.ProductName} already running", "STARTUP", MessageBoxButtons.OK, icon: MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    STAAppContext context = new STAAppContext();
                    Application.Run(context);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error");
                }
            }
        }
    }
}

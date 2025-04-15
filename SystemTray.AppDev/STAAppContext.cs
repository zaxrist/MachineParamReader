using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using SystemTray.AppDev.MouseCursor;

namespace SystemTray.AppDev
{
    public class STAAppContext : ApplicationContext
    {
        private NotifyIcon MainMenuIcon;
        private System.ComponentModel.IContainer MainMenuComponents;
        private BackgroundWorker mainWorker;

        //private ToolStripMenuItem MainMenuItemm;

        public STAAppContext()
        {
            //initlize tray viewer. Create Icon in Tray Bar and toolstrip
            MainMenuComponents = new System.ComponentModel.Container();
            MainMenuIcon = new System.Windows.Forms.NotifyIcon(MainMenuComponents)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Properties.Resources.NotReadyIcon,
                Text = $"{Application.ProductName}: Device Not Present",
                Visible = true,
            };
            MainMenuIcon.MouseClick += MainMenuIcon_MouseClick;

            mainWorker = new BackgroundWorker();
            mainWorker.DoWork += MainWorker_DoWork;

            ToolStripMenuItem StartBtn = new ToolStripMenuItem();
            StartBtn.Text = "Change Icon";
            StartBtn.Click += StartBtn_Click;
            MainMenuIcon.ContextMenuStrip.Items.Add(StartBtn);

            ToolStripMenuItem TestNotificationBtn = new ToolStripMenuItem();
            TestNotificationBtn.Text = "Test Notification Loop";
            TestNotificationBtn.Click += TestNotificationBtn_Click;
            MainMenuIcon.ContextMenuStrip.Items.Add(TestNotificationBtn);
            MainMenuIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem SettingS = new ToolStripMenuItem();
            SettingS.Text = "Settings";
            SettingS.Click += SettingS_Click; ;
            MainMenuIcon.ContextMenuStrip.Items.Add(SettingS);

            
            MainMenuIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem QuitBtn = new ToolStripMenuItem();
            QuitBtn.Text = "Exit";
            QuitBtn.Click += QuitBtn_Click;
            MainMenuIcon.ContextMenuStrip.Items.Add(QuitBtn);

            ShowMainMenuForm();
        }

        private void SettingS_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(3000);
                DisplayStatusMessage("TestNotifucatuin " + DateTime.Now);
            }
        }

        private void TestNotificationBtn_Click(object sender, EventArgs e)
        {
            // mainWorker.RunWorkerAsync();
            //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;

            //MoMove.POINT mm = new MoMove.POINT();
            //MoMove.ClientToScreen(handle, ref mm);
            //MoMove.SetCursorPos(10, 10);
            //Thread.Sleep(1000);
            //MoMove.SetCursorPos(1000, 10);
            //Thread.Sleep(1000);
            //MoMove.SetCursorPos(1000, 1000);

            MouseOperations.SetCursorPosition(1000, 0);
            Thread.Sleep(1000);
            //MessageBox.Show(MouseOperations.GetCursorPosition().X.ToString());
            Thread.Sleep(1000);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
            Thread.Sleep(1000);
            MouseOperations.SetCursorPosition(800, 0);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }

        private void MainMenuIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // MessageBox.Show("Application window open");

                ShowMainMenuForm();
            }
        }

        private void QuitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartBtn_Click(object sender, EventArgs e) //changed to change icon
        {
            MainMenuIcon.Icon = Properties.Resources.ReadyIcon;
        }
        private void DisplayStatusMessage(string text)
        {
            MainMenuIcon.BalloonTipText = text;
            MainMenuIcon.ShowBalloonTip(1000);
        }

        private bool MainWindowOpened;
        CcontrolsZax.OtherProjectWindow MainMenuWindow;
        private void ShowMainMenuForm()
        {
            //if (MainWindowOpened) { MainMenuWindow.Activate(); return; }
            //MainMenuWindow = new CcontrolsZax.OtherProjectWindow();
            //MainMenuWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //MainMenuWindow.Closed += MainMenuWindow_Closed;
            //MainMenuWindow.Show();
            //MainWindowOpened = true;
        }

        private void MainMenuWindow_Closed(object sender, EventArgs e)
        {
            MainWindowOpened = false;
            Application.Exit();
        }
    }
}

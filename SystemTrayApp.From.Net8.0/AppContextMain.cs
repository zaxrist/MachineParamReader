using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using SystemTrayApp.From.Net8._0.StudyClass;

#nullable disable
namespace SystemTrayApp.From.Net8._0
{
    public class AppContextMain : ApplicationContext
    {
        private NotifyIcon MainMenuIcon;
        private System.ComponentModel.IContainer MainMenuComponents;
        private BackgroundWorker mainWorker;
        MainThread MainTT;

        public Icon ReadyIcon = Icon.ExtractAssociatedIcon(Environment.CurrentDirectory + "\\Resources\\ReadyIcon.ico");
        public Icon NotReadyIcon = Icon.ExtractAssociatedIcon(Environment.CurrentDirectory + "\\Resources\\NotReadyIcon.ico");

        public AppContextMain()
        {
            //initlize tray viewer. Create Icon in Tray Bar and toolstrip
            MainMenuComponents = new System.ComponentModel.Container();
            MainMenuIcon = new System.Windows.Forms.NotifyIcon(MainMenuComponents)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = NotReadyIcon,
                Text = $"{System.Windows.Forms.Application.ProductName}: Device Not Present",
                Visible = true,
            };
            MainMenuIcon.MouseClick += MainMenuIcon_MouseClick;

            mainWorker = new BackgroundWorker();
            mainWorker.DoWork += MainWorker_DoWork;

            ToolStripMenuItem StartBtn = new ToolStripMenuItem();
            StartBtn.Text = "Start Thread";
            StartBtn.Click += StartBtn_Click;
            MainMenuIcon.ContextMenuStrip.Items.Add(StartBtn);

            ToolStripMenuItem TestNotificationBtn = new ToolStripMenuItem();
            TestNotificationBtn.Text = "Test";
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

            MainTT = new MainThread();
            MainTT.ProgressChanged += MainTT_ProgressChanged;
            MainTT.Start();

        }

        private void ChangeStatusIcon(string TextMsg, bool iconIsReady = true)
        {
            if (iconIsReady) { MainMenuIcon.Icon = ReadyIcon; } else { MainMenuIcon.Icon = NotReadyIcon; }
            MainMenuIcon.Text = TextMsg;

            DisplayStatusMessage(TextMsg);
        }

        private void DisableStartIfAvailable(bool disabled=false)
        {
            foreach (ToolStripItem item in MainMenuIcon.ContextMenuStrip.Items)
            {
                if(item.Text == "Start Thread")
                {
                    item.Enabled = disabled;
                }
            }
        }
        private void MainTT_ProgressChanged(object sender, MainThreadEventArg e)
        {
            if(e.Progress == Progresss.started) { ChangeStatusIcon("Background Thread Running"); DisableStartIfAvailable(false); }
            if (e.Progress == Progresss.Ended) { ChangeStatusIcon("Background Thread STOPPED",false); DisableStartIfAvailable(true); }
        }

        private void SettingS_Click(object sender, EventArgs e)
        {
            ShowMainMenuForm();
        }

        private void MainWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(3000);
                DisplayStatusMessage("TestNotifucatuin " + DateTime.Now);
            }
        }

        private void TestNotificationBtn_Click(object sender, EventArgs e) //teseter button
        {
            //dBase dd = new dBase();
            //dd.UploadData("mc", "Param", "paramval", "paramType");

            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            MouseControl.POINTT mm = new MouseControl.POINTT();
            MouseControl.ClientToScreen(handle, ref mm);
            MouseControl.SetCursorPos(10, 10);
            Thread.Sleep(1000);
            MouseControl.SetCursorPos(1000, 10);
            Thread.Sleep(1000);
            MouseControl.SetCursorPos(1000, 1000);

        }

        private void MainMenuIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               // ShowMainMenuForm();
            }
        }

        private void QuitBtn_Click(object sender, EventArgs e)
        {
            var ans = System.Windows.MessageBox.Show("QUIT APP?","QUIT",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if (ans == MessageBoxResult.Yes) { System.Windows.Forms.Application.Exit(); }
        }

        private void StartBtn_Click(object sender, EventArgs e) //changed to change icon
        {
            MainTT.Start();
        }
        private void DisplayStatusMessage(string text)
        {
            MainMenuIcon.BalloonTipText = text;
            MainMenuIcon.ShowBalloonTip(1000);
        }

        MainForm mm;
        bool WindowShown = false;
        private void ShowMainMenuForm()
        {
            if (WindowShown) { mm.Activate(); return; }

            mm = new MainForm();
            mm.MainThread = MainTT;
            mm.TopMost = true;
            mm.FormClosing += Mm_FormClosing;
            WindowShown = true;
            mm.ShowDialog();
        }

        private void Mm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowShown = false;
        }
    }
}

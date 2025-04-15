using CcontrolsZaxx.CropBox;
using System.ComponentModel;
using System.Data;
using Tesseract.WPF.NET8._0;


namespace SystemTrayApp.From.Net8._0
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            txtLbl.Text = "";
            //Looper();

            //InitParam();
            //GetParmData();
            //LoadParam();


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            xBox.Text = Settings1.Default.XCor.ToString();
            yBox.Text = Settings1.Default.YCor.ToString();
            hbox1.Text = Settings1.Default.Heightt.ToString();
            wBox.Text = Settings1.Default.Widthh.ToString();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Settings1.Default.XCor = double.Parse(xBox.Text);
                Settings1.Default.YCor = double.Parse(yBox.Text);
                Settings1.Default.Widthh = double.Parse(wBox.Text);
                Settings1.Default.Heightt = double.Parse(hbox1.Text);
                Settings1.Default.Save();
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                ScreenshotLib lb = new ScreenshotLib();
                TesserTest tt = new TesserTest();
                pictureBox1.Image = lb.TakeScreenShot1(double.Parse(xBox.Text), double.Parse(yBox.Text), double.Parse(wBox.Text), double.Parse(hbox1.Text));
                this.Show();
                txtLbl.Text = "Text Result: " + tt.GetTextT(ImageToByte(pictureBox1.Image));
                string bb = xBox.Text + "," + yBox.Text + "," + wBox.Text + "," + hbox1.Text;
                Clipboard.SetText(bb);
            }
            catch (Exception ex)
            {

            }
        }
        BackgroundWorker bg;
        public void Looper()
        {
            bg = new BackgroundWorker();
            bg.DoWork += Bg_DoWork;
            bg.WorkerSupportsCancellation = false;
            bg.RunWorkerAsync();
        }

        private void Bg_DoWork(object? sender, DoWorkEventArgs e)
        {
            ScreenshotLib lb = new ScreenshotLib();
            TesserTest tt = new TesserTest();
            ImageConverter cc = new ImageConverter();
            while (true)
            {
                System.Drawing.Image gg = lb.TakeScreenShot1(double.Parse(xBox.Text), double.Parse(yBox.Text), double.Parse(wBox.Text), double.Parse(hbox1.Text));
                if (pictureBox1.InvokeRequired)
                {
                    MethodInvoker del = delegate
                    {
                        pictureBox1.Image = gg;

                    };
                    this.Invoke(del);
                }
                if (txtLbl.InvokeRequired)
                {
                    MethodInvoker del = delegate
                    {
                        txtLbl.Text = tt.GetTextT(ImageToByte(gg));
                    };
                    this.Invoke(del);
                }
            }
        }

        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        DataTable dt;
        public void InitParam()
        {
            dt = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "ParamID";
            dt.Columns.Add(dc);

            DataColumn dc4 = new DataColumn();
            dc4.ColumnName = "Value";
            dt.Columns.Add(dc4);

        }
        public void GetParmData()
        {
            try
            {
                string set = Settings1.Default.ParmSetting;
                dt.Clear();
                var row = set.Split("`");
                foreach (var item in row)
                {
                    DataRow dataRow = dt.NewRow();
                    string split = item;
                    var s = item.Split(":");
                    dataRow[0] = s[0];
                    dataRow[1] = s[1];
                    if (s[0] != "")
                        dt.Rows.Add(dataRow);
                }
            }
            catch (Exception)
            {

            }
        }
        public void LoadParam()
        {
            dataGridView1.DataSource = dt;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string setting = "";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                setting += row.Cells["ParamID"].Value + ":" + row.Cells["Value"].Value + "`";
            }
            Settings1.Default.ParmSetting = setting;
            Settings1.Default.Save();
        }

        private string[] GetParam(string paramID1)
        {
            try
            {
                string set = Settings1.Default.ParmSetting;
                var row = set.Split("`");
                foreach (string item in row)
                {
                    string[] splitVal = item.Split(":");
                    string[] splitParmID = splitVal[0].Split("-");
                    foreach (var p in splitParmID)
                    {
                        if (p == paramID1)
                        {
                            return splitVal[1].Split(",");
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public MainThread MainThread;
        private void button2_Click(object sender, EventArgs e)
        {
            if (MainThread != null)
            {
                MainThread.Stop();
            }
        }

        private void xBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string[] tt = dataGridView1.SelectedCells[1].Value.ToString().Split(",");
                xBox.Text = tt[0];
                yBox.Text = tt[1];
                wBox.Text = tt[2];
                hbox1.Text = tt[3];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

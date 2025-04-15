using CcontrolsZaxx.CropBox;
using System.ComponentModel;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using SystemTrayApp.From.Net8._0.AppSetting;
using Tesseract;
using Tesseract.WPF.NET8._0;


namespace SystemTrayApp.From.Net8._0
{
    public enum Progresss { started, PictureReceived, ProcessingCompleted, Ended }
    public class MainThread
    {
        public event EventHandler<MainThreadEventArg> ProgressChanged;
        private System.Timers.Timer aTimer;
        private bool CanUploadData = true;
        public Progresss ThreadProgress { get; private set; }
        private BackgroundWorker BgWorker;
        public MainThread()
        {
            BgWorker = new BackgroundWorker();
            BgWorker.WorkerReportsProgress = true;
            BgWorker.WorkerSupportsCancellation = true;
            BgWorker.DoWork += BgWorker_DoWork;
            BgWorker.ProgressChanged += BgWorker_ProgressChanged;

            //init timer main
            aTimer = new System.Timers.Timer(AppSettings.Default.DataUploadInterval_Hour * 3600000);
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void ATimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            CanUploadData = true;
        }

        public void Start() {if(!BgWorker.IsBusy)BgWorker.RunWorkerAsync();}
        public void Stop() { BgWorker.CancelAsync(); }
        private void BgWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            
        }

        private void BgWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            SetProgress(Progresss.started);
            while (!BgWorker.CancellationPending)
            {
                if (CanUploadData)
                {
                    Thread.Sleep(1000);
                    RunProgram();
                }
                
            }
            SetProgress(Progresss.Ended);
        }
        private void SetProgress(Progresss progress, string statusMsg="")
        {
            ThreadProgress = progress;
            ProgressChanged?.Invoke(this,new MainThreadEventArg(progress, statusMsg));
        }

        //run program
        private void RunProgram()
        {
            string prog = Settings1.Default.ParmSetting;
            string[] ParmID = prog.Split("`");
            bool CanProceed = false;
            int upCount = 0;
            for (int i = 0; i < ParmID.Length; i++)
            {
                string[] pm = ParmID[i].Split(":");
                string[] sp = pm[0].Split("-");
                if (sp[0] == "MAT")
                {
                    string[] ss = pm[1].Split(",");
                    string res = GetText(sp[1], ProgEvalMethod.ExactMatchString, ss[4]);
                    if (res == "") { CanProceed = false; } else { CanProceed=true; }

                }
                //pre validation completed
                if(CanProceed) 
                if (sp[0] == "UPD")
                {
                    string[] ss = pm[1].Split(",");
                    if (ss[4] == "INTR")
                    {
                        string res = GetText(sp[1], ProgEvalMethod.isNumeric);
                        if (res != "")
                        {
                            if(UploadData(ss[5], res, ss[4])) { upCount++; }
                        }
                    }
                }
            }
            if ((upCount== GetUploadCounter()))
            {
                CanUploadData = false;
            }
        }

        private int GetUploadCounter()
        {
            int counter = 0;
            string prog = Settings1.Default.ParmSetting;
            string[] ParmID = prog.Split("`");
            foreach (string s in ParmID)
            {
                string[] pm = s.Split(":");
                string[] sp = pm[0].Split("-");
                if(sp[0] == "UPD") { counter++; }
            }
            return counter;
        }

        public bool UploadData(string paramName, string paramValue, string paramTypeOf)
        {
            dBase dd = new dBase();
            try
            {
                dd.UploadData(AppSettings.Default.MachineID, paramName, paramValue, paramTypeOf);
                if (dd.UploadDataSQLserver(AppSettings.Default.MachineID, paramName, paramValue, paramTypeOf))
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        //programside
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
        private string GetText(string ParamNo, ProgEvalMethod method, string CompareText = "")
        {
            TesserTest tt = new TesserTest();
            string ResultText = "";
            try
            {
                 ResultText = tt.GetTextT(ImageToByte(GetImage(ParamNo)));
            }
            catch (Exception)
            {
                return "";
            }
            string replacement = Regex.Replace(ResultText, @"\t|\n|\r", "");

            replacement= replacement.Trim();
            replacement = replacement.TrimEnd();
            replacement = replacement.TrimStart();
            replacement = replacement.ToUpper();
            switch (method)
            {
                case ProgEvalMethod.ExactMatchString:
                    try
                    {
                        if(replacement == CompareText)
                        {
                            return replacement;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                case ProgEvalMethod.isNumeric:
                    try
                    {
                        int t = int.Parse(replacement);
                        return t.ToString();
                    }
                    catch (Exception)
                    {
                        return "";
                    }
                default:return replacement;
            }
        }   
        public static byte[] ImageToByte(System.Drawing.Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        private Bitmap GetImage(string ParamNo)
        {
            string[] pm = GetParam(ParamNo);
            ScreenshotLib Lb = new ScreenshotLib();
            if(pm != null)
            {
                try
                {
                    return Lb.TakeScreenShot1(double.Parse(pm[0]), double.Parse(pm[1]), double.Parse(pm[2]), double.Parse(pm[3]));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new NotImplementedException();
        }

    }
    public enum ProgEvalMethod { ReturnAnything, ExactMatchString,isString,isNumeric}
    public class MainThreadEventArg
    {
        public MainThreadEventArg(Progresss progres, string statusMsg)
        {
            this._progres = progres;
            
        }
        private Progresss _progres;

        public Progresss Progress
        {
            get { return _progres; }
            set { _progres = value; }
        }
        private string _msg;

        public string statusMsg
        {
            get { return _msg; }
            set { _msg = value; }
        }
    }
}

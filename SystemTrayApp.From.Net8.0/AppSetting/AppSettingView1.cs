using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows.Controls;

#nullable disable
namespace SystemTrayApp.From.Net8._0.AppSetting
{
    public partial class AppSettingView1 : System.Windows.Forms.UserControl
    {
        private DataTable _Dtable;

        public DataTable Dtable
        {
            get { return _Dtable; }
            set { _Dtable = value; dataGridView1.DataSource = value; }
        }

        public AppSettingView1()
        {
            InitializeComponent();
            IntTable();
            LoadSettings();
        }
        private void IntTable()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn();
            dc.ColumnName = "SettingName";
            dt.Columns.Add(dc);

            DataColumn dc4 = new DataColumn();
            dc4.ColumnName = "Value";
            dt.Columns.Add(dc4);

            Dtable = dt;
            dataGridView1.Columns[0].ReadOnly  = true;
        }
        private void LoadSettings()
        {
            Dtable.Clear();
            foreach (SettingsProperty currentProperty in AppSettings.Default.Properties)
            {
                
                DataRow dr = Dtable.NewRow();
                dr["SettingName"] = currentProperty.Name;
                dr["value"] = AppSettings.Default[currentProperty.Name];
                Dtable.Rows.Add(dr);
            }
            Dtable = Dtable;
        }
        private void SaveSetting()
        {
            if(dataGridView1.Rows.Count < 0) { return; }
            try
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    foreach (SettingsProperty currentProperty in AppSettings.Default.Properties)
                    {
                        if (item is not null)
                            if (item.Cells[0].Value.ToString() == currentProperty.Name)
                            {
                                var propType = currentProperty.PropertyType;
                                var converter = TypeDescriptor.GetConverter(propType);
                                var convertedObject = converter.ConvertFromString(item.Cells[1].Value.ToString());

                                AppSettings.Default[currentProperty.Name] = convertedObject;
                            }
                    }
                }
                AppSettings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSetting();
            LoadSettings();
        }
    }
}

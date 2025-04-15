using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using SystemTrayApp.From.Net8._0.AppSetting;

namespace SystemTrayApp.From.Net8._0
{
    public class dBase
    {
        public bool isConnected { get; set; } = false;
        public SqliteConnection Conn;
        public dBase()
        {
            Conn = new SqliteConnection($"Data Source={AppSettings.Default.DbPath};");
            try
            {
                Conn.Open();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool UploadData(string machineID, string ParmanName,string paramValue,string paramtypeOf) //not used anymore. Migrated to SQL server
        {
            using (Conn)
            {
                SqliteCommand cmd = new SqliteCommand("INSERT INTO MoldingLoggerTbl " +
                    "(MachineID,ParamName,ParmValue,ParmTypeOf,MCTimeStamp) VALUES " +
                    "(@MachineID,@ParamName,@ParmValue,@ParmTypeOf,@MCTimeStamp)", Conn);

                try
                {
                    cmd.Parameters.AddWithValue("@MachineID", machineID);
                    cmd.Parameters.AddWithValue("@ParamName", ParmanName);
                    cmd.Parameters.AddWithValue("@ParmValue", paramValue);
                    cmd.Parameters.AddWithValue("@ParmTypeOf", paramtypeOf);
                    cmd.Parameters.AddWithValue("@MCTimeStamp", DateTime.Now);

                    int res = cmd.ExecuteNonQuery();
                    if(res > 0) {return true;}else {return false;}
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private static string XCDConnectionString = "Data Source=192.168.1.12;Initial Catalog=XCD;Persist Security Info=True;TrustServerCertificate=True;User ID=sa;Password=1n@r1swat$";

        public bool UploadDataSQLserver(string machineID, string ParmanName, string paramValue, string paramtypeOf)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(XCDConnectionString))
                {
                    string insertQuery = "INSERT INTO [dbo].[ML_MLSLOGGER] ([MachineID],[ParamName],[ParmValue],[ParmTypeOf],[MCTimeStamp]) VALUES " +
                                                                                    "(@MachineID,@ParamName,@ParmValue,@ParmTypeOf,@MCTimeStamp)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MachineID", machineID);
                        command.Parameters.AddWithValue("@ParamName", ParmanName);
                        command.Parameters.AddWithValue("@ParmValue", paramValue);
                        command.Parameters.AddWithValue("@ParmTypeOf", paramtypeOf);
                        command.Parameters.AddWithValue("@MCTimeStamp", DateTime.Now);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected.ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
    }
}

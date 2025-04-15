using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace SystemTrayApp.From.Net8._0
{

    //MOVED TO SQLITE Library
    internal class SqlServer
    {
        public static string XCDConnectionString = "Data Source=192.168.1.12;Initial Catalog=XCD;Persist Security Info=True;TrustServerCertificate=True;User ID=sa;Password=1n@r1swat$";


        public void Test()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(XCDConnectionString))
                {
                    string insertQuery = "INSERT INTO [dbo].[ML_MLSLOGGER] ([MachineID],[ParamName],[ParmValue],[ParmTypeOf],[MCTimeStamp]) VALUES " +
                                                                                    "(@MachineID,@ParamName,@ParmValue,@ParmTypeOf,@MCTimeStamp)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MachineID", "yuyu");
                        command.Parameters.AddWithValue("@ParamName", "kll");
                        command.Parameters.AddWithValue("@ParmValue", "dfgdf");
                        command.Parameters.AddWithValue("@ParmTypeOf", "jkj");
                        command.Parameters.AddWithValue("@MCTimeStamp", DateTime.Now);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}

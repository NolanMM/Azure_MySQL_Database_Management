using Cloud_Database_Management_System.Services.Security_Services.Security_Table.Data_Models;
using MySqlConnector;

namespace Cloud_Database_Management_System.Services.Security_Services.Security_Table
{
    public class Security_Table
    {
        // Class Attributes
        private static string? connect_String { get; set; }
        private static readonly string schemma = "analysis_and_reporting_security";
        private static readonly string user_id_table_name = "security_userid";
        private static readonly string password_table_name = "security_password";

        private static Security_UserId_Record? security_userId_record;
        private static Security_Password_Record? security_password_record;
        public static List<Security_UserId_Record>? Security_UserId_Record_List = new List<Security_UserId_Record>();
        public static List<Security_Password_Record>? Security_Password_Record_List = new List<Security_Password_Record>();

        private Security_Table()
        {
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        }

        public async static Task<List<Security_UserId_Record>?> ReadAllAsync_UserID_Table()
        {
            try
            {
                using MySqlConnection Connection = new MySqlConnection(connect_String);
                await Connection.OpenAsync();

                string sql = $"SELECT * FROM {schemma}.{user_id_table_name};";
                using var cmd = new MySqlCommand(sql, Connection);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var security_userid_record = new Security_UserId_Record
                    {
                        Index = reader.GetInt32(0),
                        User_ID = reader.GetString(1),
                        Email_Address = reader.GetString(2),
                    };
                    Security_UserId_Record_List.Add(security_userid_record);
                }

                //foreach (Security_UserId_Record security_userid_record in Security_UserId_Record_List)
                //{
                //}
                await Connection.CloseAsync();
                return Security_UserId_Record_List;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async static Task<List<Security_Password_Record>?> ReadAllAsync_Password_Table()
        {
            try
            {
                using MySqlConnection Connection = new MySqlConnection(connect_String);
                await Connection.OpenAsync();

                string sql = $"SELECT * FROM {schemma}.{password_table_name};";
                using var cmd = new MySqlCommand(sql, Connection);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var security_password_Record = new Security_Password_Record
                    {
                        Index_pass = reader.GetInt32(0),
                        Password = reader.GetString(1),
                    };
                    Security_Password_Record_List.Add(security_password_Record);
                }

                //foreach (Security_UserId_Record security_userid_record in Security_UserId_Record_List)
                //{
                //}
                await Connection.CloseAsync();
                return Security_Password_Record_List;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<bool> UpdateAsync()
        {
            bool isDone = false;
            try
            {
                if (Security_UserId_Record_List != null) { Security_UserId_Record_List.Clear(); }
                if (Security_Password_Record_List != null) { Security_Password_Record_List.Clear(); }
                await ReadAllAsync_UserID_Table();
                await ReadAllAsync_Password_Table();
                isDone = true;
                return isDone;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return isDone;
            }

        }

        public bool Test_Connection_To_Table()
        {
            // Safety check to make sure the connection is working properly with simple open and close
            if (connect_String != null)
            {
                try
                {
                    MySqlConnection Connection = new MySqlConnection(connect_String);
                    Connection.Open();
                    Connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Insert()
        {
            throw new NotImplementedException();
        }
    }
}

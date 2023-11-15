using Security_Services_Dev_Env.Services.Security_Services.Security_Table.Data_Models;
using Security_Services_Dev_Env.Services.Security_Services.Security_Table.Security_Tables.Security_Tables_Interface;
using MySqlConnector;

namespace Security_Services_Dev_Env.Services.Security_Services.Security_Table.Security_Tables
{
    public class security_password_table : Security_Table_Interface
    {
        private static readonly string schemma = "analysis_and_reporting_security";
        private static readonly string password_table_name = "security_password";
        private static string connect_String { get; set; } = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        public static List<Security_Data_Model_Abtraction>? Security_Password_Record_List = new List<Security_Data_Model_Abtraction>();
        public async Task<List<Security_Data_Model_Abtraction>?> ReadAllAsync_Security_Table()
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
        public async Task<bool> CreateAsync_Security_Table(Security_Data_Model_Abtraction dataModel, string tableName)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connect_String);
                await connection.OpenAsync();

                Type dataType = typeof(Security_Password_Record);

                string[] propertyNames = dataType.GetProperties()
                    .Where(p => p.Name != "Id")
                    .Select(p => p.Name)
                    .ToArray();

                string columnNames = string.Join(", ", propertyNames.Select(name => $"`{name}`"));
                string parameterNames = string.Join(", ", propertyNames.Select(name => $"@{name}"));

                string query = $"INSERT INTO `{tableName}` ({columnNames}) VALUES ({parameterNames})";

                using MySqlCommand cmd = new MySqlCommand(query, connection);

                foreach (var propertyName in propertyNames)
                {
                    var propertyValue = dataType.GetProperty(propertyName)?.GetValue(dataModel);
                    cmd.Parameters.AddWithValue($"@{propertyName}", propertyValue);
                }

                await cmd.ExecuteNonQueryAsync();
                await connection.CloseAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
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
    }
}

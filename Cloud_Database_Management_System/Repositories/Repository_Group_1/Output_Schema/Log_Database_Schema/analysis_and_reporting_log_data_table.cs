using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using MySqlConnector;

namespace Server_Side.Database_Services.Output_Schema.Log_Database_Schema
{
    public class Analysis_and_reporting_log_data_table : Output_Tables_Template
    {
        private class Log_Item
        {
            internal int Log_ID { get; set; }
            internal string? Requests { get; set; }
            internal DateTime Date_Access { get; set; }
            internal string? Data_Access { get; set; }
            internal string? Data_Content { get; set; }
            internal string? Request_Status { get; set; }
            internal string? Issues { get; set; }

        }
        // Class Attributes
        private static readonly string table_name = "analysis_and_reporting_log_data";
        private static readonly string schemma = "analysis_and_reporting_log_data";
        private static string connect_String { get; set; } = "";

        private static Analysis_and_reporting_log_data_table? analysis_and_reporting_log_data_table;
        private List<Log_Item> Log_Item_List = new List<Log_Item>();
        private Analysis_and_reporting_log_data_table()
        {
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        }

        public static async Task<Output_Tables_Template> WriteLogData_ProcessAsync(string Requests_Type, DateTime Date_Access, string Data_Access, string Data_Content, string? Request_Status, string? Issues)
        {
            analysis_and_reporting_log_data_table = new Analysis_and_reporting_log_data_table();
            if (Issues == string.Empty || Issues == null) Issues = "None";

            Log_Item logItem = new Log_Item
            {
                Requests = Requests_Type,
                Date_Access = Date_Access,
                Data_Access = Data_Access,
                Data_Content = Data_Content,
                Request_Status = Request_Status ?? "Succesful",
                Issues = Issues
            };

            bool request_status = await Create_Async(logItem);
            return analysis_and_reporting_log_data_table;
        }

        private async static Task<bool> Create_Async(Log_Item logItem)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connect_String);
                await connection.OpenAsync();

                var properties = typeof(Log_Item).GetProperties().Where(prop => prop.Name != "Log_ID").ToArray();

                string columnNames = string.Join(", ", properties.Select(prop => $"`{prop.Name}`"));
                string parameterNames = string.Join(", ", properties.Select(prop => $"@{prop.Name}"));

                string query = $"INSERT INTO {schemma}.{table_name} ({columnNames}) VALUES ({parameterNames})";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Requests", logItem.Requests);
                    cmd.Parameters.AddWithValue("@Date_Access", logItem.Date_Access);
                    cmd.Parameters.AddWithValue("@Data_Access", logItem.Data_Access);
                    cmd.Parameters.AddWithValue("@Data_Content", logItem.Data_Content);
                    cmd.Parameters.AddWithValue("@Request_Status", logItem.Request_Status);
                    cmd.Parameters.AddWithValue("@Issues", logItem.Issues);

                    using var reader = await cmd.ExecuteReaderAsync();
                }
                await connection.CloseAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<List<object>?> Read_All_Async()
        {
            try
            {
                List<Log_Item> logItemList = new List<Log_Item>();

                using MySqlConnection connection = new MySqlConnection(connect_String);
                await connection.OpenAsync();

                string sql = $"SELECT * FROM {schemma}.{table_name};";
                using MySqlCommand cmd = new MySqlCommand(sql, connection);
                using MySqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var logItem = new Log_Item
                    {
                        Log_ID = reader.GetInt32(0),
                        Requests = reader.GetString(1),
                        Date_Access = reader.GetDateTime(2),
                        Data_Access = reader.GetString(3),
                        Data_Content = reader.GetString(4),
                        Request_Status = reader.GetString(5),
                        Issues = reader.GetString(6)
                    };

                    logItemList.Add(logItem);
                }

                await connection.CloseAsync();
                List<object>? return_list = logItemList.ToList<object>();
                return return_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<object>?> Update_Async()
        {
            Log_Item_List.Clear();
            Log_Item_List = new List<Log_Item>();
            List<object>? return_list = await Read_All_Async();
            return return_list;
        }
        public bool Test_Connection_To_Table()
        {
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

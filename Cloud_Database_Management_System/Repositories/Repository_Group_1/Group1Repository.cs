using Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using MySqlConnector;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Raw_Data_Tables_Class;

namespace Cloud_Database_Management_System.Repositories.Repository_Group_1
{
    public class Group1Repository : IGroupRepository
    {
        public List<UserView> Valid_User_Views_Table;
        public List<PageView> Website_logs_table;
        public List<SaleTransaction> SalesTransactionsTable;
        public List<Feedback> FeedbackTable;
        private List<(string, string, Input_Tables_Template)> table_names_link_session_id_table_oject_list;
        public List<(string, List<List<object>>)> All_Tables_Data_Retrive_Link_With_Session_ID;
        private List<string> table_names_list;

        private readonly string Input_schemma = "analysis_and_reporting_raw_data";
        private readonly string Log_schemma = "analysis_and_reporting_log_data";

        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";
        public bool Connected_Status { get; set; } = false;

        private DateTime _created;

        private string GenerateUniqueKey()
        {
            Random random = new Random();
            int uniqueKey = random.Next(0, 65536);

            // Format the unique key as a hexadecimal string with leading zeros
            // X4 ensures it at least 4 characters with leading zeros
            string formattedKey = uniqueKey.ToString("X4");

            return formattedKey;
        }

        public Group1Repository(DateTime created)
        {
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + Input_schemma + ";SslMode=Required";
            _created = created;
            Connected_Status = false;
            Session_ID = GenerateUniqueKey();
        }

        public async Task<bool> Create(Group_Data_Model group_Data_Model, DateTime _Created, string tablename)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connect_String))
                {
                    await connection.OpenAsync();

                    Type dataType = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.FirstOrDefault(info => info.TableName.Equals(tablename, StringComparison.OrdinalIgnoreCase))?.DataType;

                    if (dataType == null)
                    {
                        Console.WriteLine("Table not found for the provided tablename.");
                        return false;
                    }

                    // Use reflection to get the property names from the data model
                    string[] propertyNames = dataType.GetProperties()
                        .Where(p => p.Name != "Id")
                        .Select(p => p.Name)
                        .ToArray();

                    // Create SQL query placeholders for column names and parameter names
                    string columnNames = string.Join(", ", propertyNames.Select(name => $"`{name}`"));
                    string parameterNames = string.Join(", ", propertyNames.Select(name => $"@{name}"));

                    // Create a new SQL command to insert data into the specified table
                    string query = $"INSERT INTO `{tablename}` ({columnNames}) VALUES ({parameterNames})";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Set command parameters based on property names
                        foreach (var propertyName in propertyNames)
                        {
                            var propertyValue = dataType.GetProperty(propertyName)?.GetValue(group_Data_Model, null);
                            cmd.Parameters.AddWithValue($"@{propertyName}", propertyValue);
                        }

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return false;
        }
        /// <summary>
        /// Read Part
        /// </summary>

        public Task<List<Group_Data_Model>> ReadTable(string tablename)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<string, object>> ReadAllTables()
        {
            try
            {
                bool result_init_table = await Initialize_Tables();
                if (result_init_table)
                {
                    All_Tables_Data_Retrive_Link_With_Session_ID = new List<(string, List<List<object>>)>();

                    foreach (var (tableName, sessionID, table) in table_names_link_session_id_table_oject_list)
                    {
                        List<object>? tableData = await table.ReadAllAsync();
                        if (tableData != null)
                        {
                            List<object> dataAsList = tableData.ToList();
                            All_Tables_Data_Retrive_Link_With_Session_ID.Add((tableName, new List<List<object>> { dataAsList }));
                        }
                    }
                    foreach (var (tableName, dataAsList) in All_Tables_Data_Retrive_Link_With_Session_ID)
                    {
                        Process_And_Print_Table_Data(dataAsList);
                    }
                    Dictionary<string, object> tables_list = new Dictionary<string, object>
                    {
                        { "Valid_User_Views_Table", Valid_User_Views_Table },
                        { "Website_logs_table", Website_logs_table },
                        { "SalesTransactionsTable", SalesTransactionsTable },
                        { "FeedbackTable", FeedbackTable }
                    };
                    return tables_list;
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }
        private async Task<bool> Initialize_Tables()
        {
            try
            {
                await Retrieve_TableName_List();

                foreach (var tableName in table_names_list)
                {
                    Input_Tables_Template table = Create_Table_ForName(tableName);

                    if (table != null)
                    {
                        // Store the table in the list along with the table name and session ID.
                        table_names_link_session_id_table_oject_list.Add((tableName, Session_ID, table));
                    }
                }
                return true;
            }
            catch { return false; }
        }
        private Input_Tables_Template? Create_Table_ForName(string tableName)
        {
            switch (tableName)
            {
                case "feedback_table":
                    return Feedback_table.SetUp(Session_ID);
                case "pageview":
                    return Pageview_table.SetUp(Session_ID);
                case "sales_transaction_table":
                    return Salestransaction_table.SetUp(Session_ID);
                case "userview":
                    return Userview_table.SetUp(Session_ID);
                default:
                    //Console.WriteLine(tableName + " template does not exist");
                    return null;
            }
        }

        public async Task<List<string>>? Retrieve_TableName_List()
        {
            string constring = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + Input_schemma + ";SslMode=Required";
            try
            {
                MySqlConnection Connection = new MySqlConnection(constring);
                Connection.Open();
                if (Connection != null)
                {
                    string sql = "SELECT table_name FROM information_schema.tables WHERE table_schema = '" + Input_schemma + "'";
                    var cmd = new MySqlCommand(sql, Connection);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var Table_Namme = reader.GetString(0);
                        table_names_list.Add(Table_Namme);
                        //Console.WriteLine($"Table_Namme: {Table_Namme}");
                    }
                }
                return table_names_list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public bool Process_And_Print_Table_Data(List<List<object>> dataAsList)
        {
            //Console.WriteLine($"{tableName} Data:");
            try
            {
                foreach (var data in dataAsList)
                {
                    foreach (var Myobject in data)
                    {
                        if (Myobject is UserView userView)
                        {
                            Valid_User_Views_Table.Add(userView);
                            //Console.WriteLine($"User_Id: {userView.User_Id}, Timestamp: {userView.Timestamp}, End_Date: {userView.End_Date}, Start_Date: {userView.Start_Date}");
                        }
                        else if (Myobject is PageView pageView)
                        {
                            Website_logs_table.Add(pageView);
                            //Console.WriteLine($"SessionId: {pageView.SessionId}, UserId: {pageView.UserId}, PageUrl: {pageView.PageUrl}, PageInfo: {pageView.PageInfo}, ProductId: {pageView.ProductId}, DateTime: {pageView.DateTime}, Start_Time: {pageView.Start_Time}, End_Time: {pageView.End_Time}");
                        }
                        else if (Myobject is SaleTransaction saleTransaction)
                        {
                            SalesTransactionsTable.Add(saleTransaction);
                            //Console.WriteLine($"TransactionId: {saleTransaction.TransactionId}, UserId: {saleTransaction.UserId}, TransactionValue: {saleTransaction.TransactionValue}, Date: {saleTransaction.Date}");
                        }
                        else if (Myobject is Feedback feedback)
                        {
                            FeedbackTable.Add(feedback);
                            //Console.WriteLine($"FeedbackId: {feedback.FeedbackId}, UserId: {feedback.UserId}, ProductId: {feedback.ProductId}, StarRating: {feedback.StarRating}");
                        }
                        else
                        {
                            Console.WriteLine("Unknown object type");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Done Read Part
        /// </summary>
        public bool Update()
        {
            throw new NotImplementedException();
        }
        public bool Delete()
        {
            throw new NotImplementedException();
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
                    Connected_Status = true;
                    Connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Connected_Status = false;
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

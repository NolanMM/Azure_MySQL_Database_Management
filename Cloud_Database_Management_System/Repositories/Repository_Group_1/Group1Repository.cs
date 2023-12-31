﻿using Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using MySqlConnector;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Raw_Data_Tables_Class;
using System.IO;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;

namespace Cloud_Database_Management_System.Repositories.Repository_Group_1
{
    public class Group1Repository : IGroupRepository
    {
        public List<UserView> Valid_User_Views_Table = new List<UserView>();
        public List<PageView> Website_logs_table = new List<PageView>();
        public List<SaleTransaction> SalesTransactionsTable = new List<SaleTransaction>();
        public List<Feedback> FeedbackTable = new List<Feedback>();
        private List<(string, string, Input_Tables_Template)> table_names_link_session_id_table_oject_list = new List<(string, string, Input_Tables_Template)>();
        public List<(string, List<List<object>>)> All_Tables_Data_Retrive_Link_With_Session_ID = new List<(string, List<List<object>>)>();
        private List<string> table_names_list = new List<string>();

        private readonly string Input_schemma = "analysis_and_reporting_raw_data";
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
            connect_String = "server=databasesystemmodule1.mysql.database.azure.com; uid=nolanmdatabasemanager;pwd=Conkhunglongtovai1;database=" + Input_schemma + ";SslMode=Required";
            _created = created;
            Connected_Status = false;
            Session_ID = GenerateUniqueKey();
        }

        public async Task<bool> Create(Group_Data_Model group_Data_Model, DateTime _Created, string tablename)
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connect_String);
                await connection.OpenAsync();

                Type? dataType = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type
                    .FirstOrDefault(info => info.TableName.Equals(tablename, StringComparison.OrdinalIgnoreCase))?.DataType;
                if (dataType == null)
                {
                    Console.WriteLine("Table not found for the provided tablename.");
                    return false;
                }

                string[] propertyNames = dataType.GetProperties()
                    .Where(p => p.Name != "Id")
                    .Select(p => p.Name)
                    .ToArray();

                string columnNames = string.Join(", ", propertyNames.Select(name => $"`{name}`"));
                string parameterNames = string.Join(", ", propertyNames.Select(name => $"@{name}"));

                string query = $"INSERT INTO `{tablename}` ({columnNames}) VALUES ({parameterNames})";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    // Set command parameters based on property names
                    foreach (var propertyName in propertyNames)
                    {
                        var propertyValue = dataType.GetProperty(propertyName)?.GetValue(group_Data_Model);
                        cmd.Parameters.AddWithValue($"@{propertyName}", propertyValue);
                    }

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        string dataContent = group_Data_Model.ToString();
                        await LogSuccess("POST", tablename, dataContent);
                    }

                    await connection.CloseAsync();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                string dataContent = "Error: " + ex.Message;
                string requestType = "ErrorInGroup1RepositoryCreate";
                await LogErrorInGroup1RepositoryCreate(ex, tablename, dataContent, requestType);
                return false;
            }
        }

        private async Task LogSuccess(string requestType, string tablename, string dataContent)
        {
            string requestStatus = "Successful";
            bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                requestType,
                DateTime.Now,
                tablename,
                dataContent,
                requestStatus,
                null
            );

            if (!logStatus)
            {
                Console.WriteLine("Error: Unable to log success.");
            }
        }

        private async Task LogErrorInGroup1RepositoryCreate(Exception? ex, string tablename, string dataContent, string requestType)
        {
            string requestStatus = "Failed";
            bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                requestType,
                DateTime.Now,
                tablename,
                dataContent,
                requestStatus,
                ex.Message
            );

            if (!logStatus)
            {
                Console.WriteLine("Error: Unable to log error.");
            }
        }

        private static void LogError_LocalFile(string tablename, string errorMessage)
        {
            try
            {
                string logFilePath = @"C:\Users\Minh\Desktop\Log_Errors.txt";

                // Format the data and time for logging
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Error writing to {tablename} table: {errorMessage}\n";

                // Append the log entry to the file
                File.AppendAllText(logFilePath, logEntry);
            }
            catch (Exception logEx)
            {
                Console.WriteLine("Error logging error: " + logEx.Message);
            }
        }

        /// <summary>
        /// Read Part
        /// </summary>

        public async Task<object?> ReadTable(string tablename)
        {
            try
            {
                if (string.IsNullOrEmpty(tablename))
                {
                    return null;
                }

                bool result_init_table = await Initialize_Tables();
                if (result_init_table)
                {
                    var tableInfo = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type_Table_Type.Find(t => t.TableName == tablename);

                    if (tableInfo != null)
                    {
                        Input_Tables_Template? table = tableInfo.TableType;

                        List<object>? tableData = await table.ReadAllAsync();

                        if (tableData != null)
                        {
                            List<List<object>> dataAsList = tableData.Select(item => new List<object> { item }).ToList();
                            bool processingResult = await Process_And_Print_Table_DataAsync(dataAsList);
                            object? result = UpdateTableNameAndListData(tableInfo.TableName);

                            string dataContent = "Table read operation successful";
                            await LogSuccess("GET", tablename, dataContent);

                            return result;
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                string dataContent = "Error: " + ex.Message;
                string requestType = "ErrorInReadTable";
                await LogErrorInGroup1RepositoryCreate(ex, tablename, dataContent, requestType);
                return null;
            }
        }

        private object? UpdateTableNameAndListData(string tablename)
        {
            // Create a dictionary to map table names to their corresponding lists
            Dictionary<string, object> Tablename_ListData_Dict = new Dictionary<string, object>
            {
                { "userview", Valid_User_Views_Table },
                { "pageview", Website_logs_table },
                { "sales_transaction_table", SalesTransactionsTable },
                { "feedback_table", FeedbackTable }
            };

            if (Tablename_ListData_Dict.ContainsKey(tablename))
            {
                object tableData = Tablename_ListData_Dict[tablename];
                return tableData;
            }
            return null;
        }
        public async Task<Dictionary<string, object>?> ReadAllTables()
        {
            try
            {
                bool resultInitTable = await Initialize_Tables();
                if (resultInitTable)
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
                        Process_And_Print_Table_DataAsync(dataAsList);
                    }

                    Dictionary<string, object> tablesList = new Dictionary<string, object>
                    {
                        { "Valid_User_Views_Table", Valid_User_Views_Table },
                        { "Website_logs_table", Website_logs_table },
                        { "SalesTransactionsTable", SalesTransactionsTable },
                        { "FeedbackTable", FeedbackTable }
                    };

                    string dataContent = "All tables read operation successful";
                    await LogSuccess("GET", "AllTables", dataContent);

                    return tablesList;
                }
                else
                {
                    string dataContent = "Error: Initialization of tables failed";
                    await LogErrorInGroup1RepositoryCreate(null, "AllTables", dataContent, "GET");
                    return null;
                }
            }
            catch (Exception ex)
            {
                string dataContent = "Error: " + ex.Message;
                await LogErrorInGroup1RepositoryCreate(ex, "AllTables", dataContent, "GET");
                return null;
            }
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

        public async Task<List<string>?> Retrieve_TableName_List()
        {
            string constring = "server=databasesystemmodule1.mysql.database.azure.com; uid=nolanmdatabasemanager;pwd=Conkhunglongtovai1;database=" + Input_schemma + ";SslMode=Required";
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
                string dataContent = "Error: " + ex.Message;
                await LogErrorInGroup1RepositoryCreate(ex, "Retrieve_TableName_List", dataContent, "GET");
                return null;
            }
        }
        public async Task<bool> Process_And_Print_Table_DataAsync(List<List<object>> dataAsList)
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
                string dataContent = "Error: " + ex.Message;
                await LogErrorInGroup1RepositoryCreate(ex, "Process_And_Print_Table_DataAsyncErr", dataContent, "DATA_PROCESS");
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

        public async Task<bool> Test_Connection_To_TableAsync()
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
                    string dataContent = "Error: " + ex.Message;
                    await LogErrorInGroup1RepositoryCreate(ex, "Test_Connection_To_TableAsyncErr", dataContent, "CONNECTION");
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

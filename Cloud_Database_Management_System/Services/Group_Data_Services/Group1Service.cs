using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables;
using Cloud_Database_Management_System.Repositories.Repository_Group_1;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group1Service : IGroupService
    {
        private IGroupRepository _Group1Repository;
        private Group_Data_Model _Group1_Data_Model;
        private DateTime _Created;

        public Group1Service(DateTime created, object data)
        {
            _Group1Repository = new Group1Repository(created);
            _Created = created;
        }

        public Group1Service(DateTime created)
        {
            _Group1Repository = new Group1Repository(created);
            _Created = created;
        }
        public async Task<object> ProcessGetRequestAllDataTablesCorrespondGroupID()
        {
            try
            {
                Dictionary<string, object> ResultsAllTables = await _Group1Repository.ReadAllTables();
                return ResultsAllTables;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        public async Task<object> ProcessGetRequestDataCorrespondGroupID(int tablenumber)
        {
            try
            {
                if (tablenumber >= 0 && tablenumber < Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count)
                {
                    string tablename = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type[tablenumber].TableName;
                    object resultsAllDataInTables = await _Group1Repository.ReadTable(tablename);
                    return resultsAllDataInTables;
                }
                else
                {
                    return "Invalid tablenumber";
                }
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<bool> ProcessPostRequestDataCorrespondGroupIDAsync(object data, int tableNumber)
        {
            try
            {
                _Group1_Data_Model = await ProcessDataForGroup1(data, tableNumber) as Group_Data_Model;
                Table_Group_1_Dictionary tableInfo = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.FirstOrDefault(info => info.Index == tableNumber);
                // Dont need to log because the error already be log on during the ProcessDataForGroup1 function
                if (tableInfo == null || _Group1_Data_Model == null)
                {
                    return false;
                }

                return await _Group1Repository.Create(_Group1_Data_Model, _Created, tableInfo.TableName);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error processing post request data: " + ex.Message);
                //LogError("PostRequestDataProcessing", ex.Message);
                string dataString = JsonSerializer.Serialize(data);
                string request_type = "ProcessPostDataCorrespondGroupIDAsyncErr";
                string Issues = ex.Message;
                string Request_Status = "Failed";
                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                        request_type,
                        DateTime.Now,
                        tableNumber.ToString(),
                        dataString,
                        Request_Status,
                        Issues
                    );
                if (logStatus)
                {
                    Console.WriteLine("Error processing post request data: " + ex.Message);
                    return false;
                }
                else
                {
                    Console.WriteLine("Error processing post request data: " + ex.Message);
                    return false;
                }
            }
        }

        private async static Task<Group_Data_Model?> ProcessDataForGroup1(object data, int tableNumber)
        {
            if (data == null) { return null; }

            Table_Group_1_Dictionary tableInfo = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.FirstOrDefault(info => info.Index == tableNumber);

            if (tableInfo == null)
            {
                return null;
            }

            Type dataType = tableInfo.DataType;
            string tableName = tableInfo.TableName;

            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Converters = { new DateOnlyConverter() },
                };

                switch (tableNumber)
                {
                    case 0:
                        if (dataType == typeof(UserView))
                        {
                            UserView userView = JsonSerializer.Deserialize<UserView>(data.ToString(), options);
                            if (await ValidateDataAnnotations(userView, tableNumber))
                            {
                                return (Group_Data_Model?)userView;
                            }
                            else { return null; }
                        }
                        break;

                    case 1:
                        if (dataType == typeof(PageView))
                        {
                            PageView pageView = JsonSerializer.Deserialize<PageView>(data.ToString(), options);
                            if (await ValidateDataAnnotations(pageView, tableNumber))
                            {
                                return (Group_Data_Model?)pageView;
                            }else { return null; }
                        }
                        break;

                    case 2:
                        if (dataType == typeof(SaleTransaction))
                        {
                            SaleTransaction saleTransaction = JsonSerializer.Deserialize<SaleTransaction>(data.ToString(), options);
                            if (await ValidateDataAnnotations(saleTransaction, tableNumber))
                            {
                                return (Group_Data_Model?)saleTransaction;
                            }else { return null; }
                        }
                        break;

                    case 3:
                        if (dataType == typeof(Feedback))
                        {
                            Feedback feedback = JsonSerializer.Deserialize<Feedback>(data.ToString(), options);
                            if (await ValidateDataAnnotations(feedback, tableNumber))
                            {
                                return (Group_Data_Model?)feedback;
                            }
                            else { return null; }
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (JsonException ex)
            {
                string dataString = JsonSerializer.Serialize(data);
                string request_type = "ProcessPostDataCorrespondGroupIDAsyncErr";
                string Issues = ex.Message;
                string Request_Status = "Failed";
                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                        request_type,
                        DateTime.Now,
                        tableNumber.ToString(),
                        dataString,
                        Request_Status,
                        Issues
                    );
                if (logStatus)
                {
                    Console.WriteLine("Error processing post request data: " + ex.Message);
                    return null;
                }
                else
                {
                    Console.WriteLine("Error processing post request data: " + ex.Message);
                    return null;
                }
            }
            return null;
        }
        private async static Task<bool> ValidateDataAnnotations(object obj, int tableNumber)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, context, results, validateAllProperties: true);

            if (!isValid)
            {
                StringBuilder errorMessageBuilder = new StringBuilder();
                foreach (var validationResult in results)
                {
                    errorMessageBuilder.AppendLine($"Validation error: {validationResult.ErrorMessage}");
                }
                string combinedErrorMessage = errorMessageBuilder.ToString();

                string dataString = JsonSerializer.Serialize(obj);
                string request_type = "Validate DataAnnotations Error";
                string Issues = "Not pass the DataAnnotations check for the data model input: " + combinedErrorMessage;
                string Request_Status = "Failed";
                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                        request_type,
                        DateTime.Now,
                        tableNumber.ToString(),
                        dataString,
                        Request_Status,
                        Issues
                    );
                if (logStatus)
                {
                    Console.WriteLine("Not pass the DataAnnotations check for the data model input");
                    return isValid;
                }
                else
                {
                    Console.WriteLine("Not pass the DataAnnotations check for the data model input");
                    return isValid;
                } 
            }

            return isValid;
        }
        private static void LogError(string logType, string errorMessage)
        {
            try
            {
                string logFilePath = @"C:\Users\Minh\Desktop\Log_Errors.txt";
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {logType} Error: {errorMessage}\n";
                File.AppendAllText(logFilePath, logEntry);
            }
            catch (Exception logEx)
            {
                Console.WriteLine("Error logging error: " + logEx.Message);
            }
        }
    }
}
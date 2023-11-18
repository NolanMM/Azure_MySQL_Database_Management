using Security_Services_Dev_Env.Services.Security_Services.Hashing_Services;
using Security_Services_Dev_Env.Services.Security_Services.Security_Table.Data_Models;
using Security_Services_Dev_Env.Services.Security_Services.Security_Table;
using System.Text.RegularExpressions;
using Security_Services_Dev_Env.Services.Security_Services.AES_Services;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using Security_Services_Dev_Env.Database_Services.Output_Schema.Log_Database_Schema;
using Security_Services_Dev_Env.Security_Services.OTP_Services;
using Security_Services_Dev_Env.Security_Services.Security_Table.Data_Models;
using MySqlConnector;

namespace Security_Services_Dev_Env.Services.Security_Services
{
    public static class Security_Database_Services_Centre
    {
        private static string? connection_string { get; set; }
        private static string? Security_Schema { get; set; }
        public static List<Security_UserId_Record> Security_UserId_Record_List = new List<Security_UserId_Record>();
        public static List<Security_Password_Record> Security_Password_Record_List = new List<Security_Password_Record>();
        public static List<OTP_Record> Security_OTP_Record_List = new List<OTP_Record>();
        private static readonly string user_id_table_name = "security_userid";
        private static readonly string password_table_name = "security_password";
        private static readonly string otp_table_name = "otp_table";
        public async static Task<bool> SignInProcess(string username, string password)
        {
            bool isValid = false;
            if (Checking_Input_SignIn(username, password))
            {
                if (await UpdateAsyncDataAsync())
                {
                    // Retrieve the counter
                    int index_temp = Security_UserId_Record_List.Count();

                    // hasing rawkey and take first 16 character for the input for AES 128bit
                    string hasing_value = Hasing_Services.HashString(username);

                    // Check if hasing_value == any UserID value
                    Security_UserId_Record? account_record = Security_UserId_Record_List.FirstOrDefault(info => info.User_ID == hasing_value);
                    if (account_record != null) // account exist check the password
                    {
                        string key = hasing_value.Substring(0, 8 - CountDigits(index_temp)) + account_record.Index_UserID.ToString();

                        // Encrypt the password with the key
                        string encrypted_password = AES_Services_Control.Encrypt(password, key);
                        Security_Password_Record? password_record = Security_Password_Record_List.FirstOrDefault(info => info.Index_pass == account_record.Index_UserID);

                        if (password_record != null)
                        {
                            string password_in_DB = password_record.Password;
                            string decrypted_password = AES_Services_Control.Decrypt(encrypted_password, key);

                            if (encrypted_password == password_in_DB) {

                                isValid = true;
                                return isValid;
                            }
                            else
                            {
                                isValid = false;    // Wrong Password
                                return isValid;
                            }

                        } else
                        {
                            isValid = false;    // Error with the index inside the Security table
                            return isValid;
                        }
                    }
                    else
                    {
                        isValid = false;    // Wrong username/not found
                        return isValid;
                    }
                }
                else
                {
                    isValid = false;     // Fail update the data
                    return isValid;
                }
            }
            isValid = false;        // Fail checking input
            return isValid;
        }
        public async static Task<OTP_Record?> SignUpProcess_Begin(string username, string email, string password)
        {
            OTP_Record? result = null;

            // Check for the input first no special character
            if (Checking_Input_Register(username, email, password))
            {
                if (await UpdateAsyncDataAsync())
                {
                    // Retrieve the counter
                    int index_temp = Security_UserId_Record_List.Count() + 1;
                    // hasing rawkey and take first 16 character for the input for AES 128bit
                    string hasing_value = Hasing_Services.HashString(username);
                    // Check if hasing_value == any UserID value
                    Security_UserId_Record? account_record = Security_UserId_Record_List.FirstOrDefault(info => info.User_ID == hasing_value);
                    if (account_record == null)
                    {
                        // Create the key for the string
                        string key = hasing_value.Substring(0, 8 - CountDigits(index_temp)) + index_temp.ToString();
                        // Encrypt the password with the key
                        string encrypted_password = AES_Services_Control.Encrypt(password, key);

                        // Store the information of new account to the database into 2 tables
                        Security_UserId_Record security_UserId_Record = new Security_UserId_Record
                        {
                            Index_UserID = index_temp,
                            User_ID = hasing_value,
                            Email_Address = email
                        };
                        Security_Password_Record Security_password_Record = new Security_Password_Record
                        {
                            Index_pass = index_temp,
                            Password = encrypted_password
                        };

                        // Check the input for database
                        bool Check_Username_Input_Valid = await ValidateDataAnnotations(security_UserId_Record, user_id_table_name);
                        bool Check_Password_Input_Valid = await ValidateDataAnnotations(Security_password_Record, password_table_name);
                        if (Check_Username_Input_Valid && Check_Password_Input_Valid)
                        {
                            try
                            {
                                // Generate OTP code by hasing the email of user this is the token key to sign in faster
                                string raw_OTP_code = Hasing_Services.HashString(email);
                                string OTP_code = raw_OTP_code.Substring(0, 16);
                                string OTP_ID = raw_OTP_code.Substring(16, 20);

                                // Send the OTP to the user make sign up request
                                bool isSend = await OTP_Module_Services.Send_OTP_CodeAsync(OTP_code, email);
                                // if sent successful
                                if (isSend)
                                {
                                    // Create a record in the otp table to store the otp code to validate later
                                    OTP_Record OTP_record = new OTP_Record
                                    {
                                        OTP_Index = index_temp,
                                        OTP_Code = OTP_code,
                                        User_ID = hasing_value,
                                        Email_Address = email,
                                        Password = encrypted_password,
                                        Time_Created = DateTime.Now,
                                        OTP_ID = OTP_ID
                                    };
                                    // Write the data to the data table
                                    bool Check_OTP_Input_Valid = await ValidateDataAnnotations(OTP_record, otp_table_name);
                                    if (Check_OTP_Input_Valid)
                                    {
                                        result = OTP_record;
                                        return result;
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                } else
                                {
                                    return null;
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                return null;   // Write Error
                            }
                        }
                        else
                        {
                            return null; // In valid input for length
                        }
                    }
                    else   // else account has been created before -> return false
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        public async static Task<bool> OTP_Table_Record_Process(OTP_Record? OTP_record)
        {
            if(OTP_record == null)
            {
                return false;
            }
            bool create_otp_Record = await Security_Table_DB_Control.CreateAsyncTablename(OTP_record, otp_table_name);
            return create_otp_Record;
        }
        public async static Task<bool> SignUpProcessFinish(string OTP_ID, string OTP_code)
        {
            bool isValid = false;
            // Checking the input 
            bool valid_OTP_ID = IsInvalidInput(OTP_ID, "OTP_ID");
            bool valid_OTP_Code = IsInvalidInput(OTP_code, "OTP_CODE");
            if(valid_OTP_Code &&  valid_OTP_ID)
            {
                // Update the OTP list
                if(await UpdateAsyncDataOTPAsync())
                {
                    OTP_Record? OTP_record = Security_OTP_Record_List.FirstOrDefault(info => info.OTP_ID == OTP_ID);
                    if(OTP_record != null)
                    {
                        // Check for the OTP_code
                        if(OTP_record.OTP_Code == OTP_code)
                        {
                            // Create valid account to write to database
                            Security_UserId_Record security_UserId_Record = new Security_UserId_Record
                            {
                                Index_UserID = OTP_record.OTP_Index,
                                User_ID = OTP_record.User_ID,
                                Email_Address = OTP_record.Email_Address
                            };
                            Security_Password_Record Security_password_Record = new Security_Password_Record
                            {
                                Index_pass = OTP_record.OTP_Index,
                                Password = OTP_record.Password
                            };

                            //Write the data to the database
                            bool create_accound_ID = await Security_Table_DB_Control.CreateAsyncTablename(security_UserId_Record, user_id_table_name);
                            bool create_password_ID = await Security_Table_DB_Control.CreateAsyncTablename(Security_password_Record, password_table_name);

                            if (create_accound_ID && create_password_ID)
                            {
                                isValid = true;
                                return isValid;
                            }
                            else
                            {
                                isValid = false;
                                return isValid;
                            }
                        }else
                        {
                            isValid = false;
                            return false;
                        }
                    }
                    else
                    {
                        isValid = false;    // Wrong OTP_ID // Exceed Time - 5 mins
                        return isValid;
                    }
                }
                else
                {
                    isValid = false;    // Failed to update log list
                    return isValid;
                }
            }
            return isValid;     // Failed for the input
        }

        private static async Task<bool> UpdateAsyncDataOTPAsync()
        {
            Security_OTP_Record_List = new List<OTP_Record>();
            try
            {
                List<Security_Data_Model_Abtraction>? OTP_List = await Security_Table_DB_Control.ReadAllAsyncTablename(otp_table_name);
                Security_OTP_Record_List = OTP_List?.Cast<OTP_Record>().ToList();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        private static async Task<bool> UpdateAsyncDataAsync()
        {
            Security_UserId_Record_List = new List<Security_UserId_Record>();
            Security_Password_Record_List = new List<Security_Password_Record>();
            try
            {
                List<Security_Data_Model_Abtraction>? userID_List = await Security_Table_DB_Control.ReadAllAsyncTablename(user_id_table_name);
                Security_UserId_Record_List = userID_List?.Cast<Security_UserId_Record>().ToList();

                List<Security_Data_Model_Abtraction>? password_List = await Security_Table_DB_Control.ReadAllAsyncTablename(password_table_name);
                Security_Password_Record_List = password_List?.Cast<Security_Password_Record>().ToList();
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        private async static Task<bool> ValidateDataAnnotations(object? obj, string tablename)
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
                string requestType = "Validate DataAnnotations Error";
                string issues = "Not pass the DataAnnotations check for the data model input: " + combinedErrorMessage;
                string requestStatus = "Failed";

                await LogError(requestType, tablename, dataString, requestStatus, issues);

                Console.WriteLine("Not pass the DataAnnotations check for the data model input");
                return isValid;
            }

            return isValid;
        }

        static int CountDigits(int number)
        {
            string numberString = Math.Abs(number).ToString();
            return numberString.Length;
        }

        public static bool Checking_Input_Register(string username, string email, string password)
        {
            bool isValid = true;
            if (!IsInvalidInput(username, "Username"))
            {
                isValid = false;
            }

            if (!IsInvalidEmail(email))
            {
                isValid = false;
            }

            if (!IsInvalidInput(password, "Password"))
            {
                isValid = false;
            }

            return isValid;
        }
        public static bool Checking_Input_SignIn(string username, string password)
        {
            bool isValid = true;
            if (!IsInvalidInput(username, "Username"))
            {
                isValid = false;
            }

            if (!IsInvalidInput(password, "Password"))
            {
                isValid = false;
            }

            return isValid;
        }
        
        private static bool IsInvalidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email is null or empty.");
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(email, pattern))
            {
                Console.WriteLine("Invalid email format.");
                return false;
            }

            return true;
        }

        private static bool IsInvalidInput(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{fieldName} is null or empty.");
                return false;
            }

            string pattern = @"[^a-zA-Z0-9]";

            if (!Regex.IsMatch(input, pattern))
            {
                Console.WriteLine($"{fieldName} contains special characters.");
                return false;
            }

            return true;
        }
        private async static Task LogError(string requestType, string tableNumber, string dataString, string requestStatus, string issues)
        {
            bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                requestType,
                DateTime.Now,
                tableNumber,
                dataString,
                requestStatus,
                issues
            );

            if (!logStatus)
            {
                Console.WriteLine("Error: Unable to log error.");
            }
        }
    }
}

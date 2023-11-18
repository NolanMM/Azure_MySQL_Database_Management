using MySqlConnector;
using OTP_Centre.DataModel;

namespace OTP_Centre.Tables
{
    public class security_otp_table
    {
        private static readonly string schemma = "analysis_and_reporting_security";
        private static readonly string otp_table_name = "otp_table";
        private static readonly int Time_Delay_In_Mins = 1;
        private static string connect_String { get; set; } = "server=databasesystemmodule1.mysql.database.azure.com; uid=nolanmdatabasemanager;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        public static List<OTP_Record>? Security_OTP_Record_List = new List<OTP_Record>();
        public async Task<bool> CreateAsync_Security_Table(OTP_Record dataModel, string tableName)
        {
            try
            {
                bool result = await Task.Run(async () =>
                {
                    using MySqlConnection connection = new MySqlConnection(connect_String);
                    await connection.OpenAsync();

                    Type dataType = typeof(OTP_Record);

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

                    // Delay asynchronously without blocking the main thread
                    await Task.Delay(TimeSpan.FromMinutes(Time_Delay_In_Mins));

                    // Execute the command to check for the time and auto-delete the record
                    // Take the value of otpID
                    string OTP_ID_Property = "OTP_ID";
                    string? OTP_ID_Value = (dataType.GetProperty(OTP_ID_Property)?.GetValue(dataModel)) as string;
                    if (OTP_ID_Value != null)
                    {
                        bool isDeleted = await DeletebyOTPID(OTP_ID_Value);
                        if (isDeleted)
                        {
                            // The record deleted correctly
                            await connection.CloseAsync();
                            return true;
                        }
                        else
                        {
                            await connection.CloseAsync();
                            Console.WriteLine("Auto deletion of expired OTP record failed.");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error retrieving the OTP_ID");
                    }

                    await connection.CloseAsync();
                    return true;
                });

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }



        public async Task<List<OTP_Record>?> ReadAllAsync_Security_Table()
        {
            Security_OTP_Record_List = new List<OTP_Record>();
            try
            {
                using MySqlConnection Connection = new MySqlConnection(connect_String);
                await Connection.OpenAsync();

                string sql = $"SELECT * FROM {schemma}.{otp_table_name};";
                using var cmd = new MySqlCommand(sql, Connection);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var OTP_record = new OTP_Record
                    {
                        OTP_Index = reader.GetInt32(0),
                        OTP_Code = reader.GetString(1),
                        User_ID = reader.GetString(2),
                        Email_Address = reader.GetString(3),
                        Password = reader.GetString(4),
                        Time_Created = reader.GetDateTime(5),
                        OTP_ID = reader.GetString(6)
                    };
                    Security_OTP_Record_List.Add(OTP_record);
                }
                //foreach (Security_UserId_Record security_userid_record in Security_UserId_Record_List)
                //{
                //}
                await Connection.CloseAsync();
                return Security_OTP_Record_List;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
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

        private static async Task<bool> DeleteExpiredOTPRecordsAsync_Process(string? otpId, MySqlConnection connection)
        {
            if (!string.IsNullOrEmpty(otpId))
            {
                try
                {
                    using var command = new MySqlCommand(
                        "DELETE FROM " + otp_table_name + " WHERE Time_Created < NOW() - INTERVAL " + Time_Delay_In_Mins + " MINUTE AND OTP_ID = @OTP_ID",
                        connection
                    );

                    command.Parameters.AddWithValue("@OTP_ID", otpId);

                    await command.ExecuteNonQueryAsync();
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
        public static async Task<bool> DeletebyOTPID(string? OTP_ID)
        {
            try
            {
                using MySqlConnection Connection = new MySqlConnection(connect_String);
                await Connection.OpenAsync();

                string sql = $"DELETE FROM {schemma}.{otp_table_name} WHERE OTP_ID = @OTP_ID;";
                using var cmd = new MySqlCommand(sql, Connection);
                cmd.Parameters.AddWithValue("@OTP_ID", OTP_ID);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                await Connection.CloseAsync();

                // Check if any rows were affected
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}


﻿using Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using MySqlConnector;

namespace Cloud_Database_Management_System.Repositories.Repository_Group_1.Raw_Data_Tables_Class
{
    public class Userview_table : Input_Tables_Template
    { 
        // Class Attributes
        private readonly string table_name = "userview";
        private readonly string schemma = "analysis_and_reporting_raw_data";
        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";

        private bool Connected_Status { get; set; } = false;
        private bool Created_Status { get; set; } = false;

        private static Userview_table? userview_Table;
        private List<UserView> userViews = new List<UserView>();
        private Userview_table(string session_ID)
        {
            Created_Status = true;
            Connected_Status = false;
            Session_ID = session_ID;
            connect_String = "server=databasesystemmodule1.mysql.database.azure.com; uid=nolanmdatabasemanager;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        }

        public static Input_Tables_Template SetUp(string session_ID)
        {
            userview_Table = new Userview_table(session_ID);
            return userview_Table;
        }

        public async Task<List<object>?> ReadAllAsync()
        {
            userViews.Clear();
            try
            {
                using MySqlConnection Connection = new MySqlConnection(connect_String);
                await Connection.OpenAsync();
                Connected_Status = true;

                string sql = $"SELECT * FROM {schemma}.{table_name};";
                using var cmd = new MySqlCommand(sql, Connection);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var userView = new UserView
                    {
                        UserView_ID = reader.GetInt32("UserView_ID"),
                        User_ID = reader.GetString("User_ID"),
                        Product_ID = reader.GetString("Product_ID"),
                        Time_Count = reader.GetDecimal("Time_Count"),
                        Date_Access = reader.GetDateTime("Date_Access")
                    };
                    userViews.Add(userView);
                }

                //foreach (UserView userView in userViews)
                //{
                //    Console.WriteLine($"SessionId: {Session_ID}, User_Id: {userView.User_Id}, Start_Date: {userView.Start_Date}, End_Date: {userView.End_Date}, Timestamp: {userView.Timestamp}");
                //}
                await Connection.CloseAsync();
                return userViews.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                Connected_Status = false;
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task UpdateAsync()
        {
            userViews.Clear();
            userViews = new List<UserView>();
            await ReadAllAsync();
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

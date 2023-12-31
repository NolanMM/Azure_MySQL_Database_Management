﻿using Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using MySqlConnector;

namespace Cloud_Database_Management_System.Repositories.Repository_Group_1.Raw_Data_Tables_Class
{
    public class Feedback_table : Input_Tables_Template
    {
        // Class Attributes
        private readonly string table_name = "feedback_table";
        private readonly string schemma = "analysis_and_reporting_raw_data";
        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";

        public bool Connected_Status { get; set; } = false;
        private bool Created_Status { get; set; } = false;

        private static Feedback_table? feedback_table;
        private List<Feedback> feedback_list = new List<Feedback>();
        private Feedback_table(string session_ID)
        {
            Created_Status = true;
            Connected_Status = false;
            Session_ID = session_ID;
            connect_String = "server=databasesystemmodule1.mysql.database.azure.com; uid=nolanmdatabasemanager;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        }

        public static Input_Tables_Template SetUp(string session_ID)
        {
            feedback_table = new Feedback_table(session_ID);
            return feedback_table;
        }
        public async Task<List<object>?> ReadAllAsync()
        {
            feedback_list.Clear();
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
                    var feedback = new Feedback
                    {
                        Feedback_ID = reader.GetInt32(0),
                        User_ID = reader.GetString(1),
                        Product_ID = reader.GetString(2),
                        Stars_Rating = reader.GetDecimal(3),
                        Date_Updated = reader.GetDateTime(4),
                    };
                    feedback_list.Add(feedback);
                }

                //foreach (Feedback feedback in feedback_list)
                //{
                //    Console.WriteLine($"SessionId: {Session_ID}, FeedbackId: {feedback.FeedbackId}, UserId: {feedback.UserId}, ProductId: {feedback.ProductId}, StarRating: {feedback.StarRating}");
                //}
                await Connection.CloseAsync();
                return feedback_list.Cast<object>().ToList();
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
            feedback_list.Clear();
            feedback_list = new List<Feedback>();
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

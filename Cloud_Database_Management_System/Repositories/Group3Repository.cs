using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Repositories
{
    public class Group3Repository : IGroupRepository
    {
        private DateTime _created;
        private Group3_Data_Model _Group3_Data_Model;

        private readonly string table_name = "group3_table";
        private readonly string schemma = "analysis_and_reporting_raw_data";
        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";
        public bool Connected_Status { get; set; } = false;
        private bool Created_Status { get; set; } = false;
        // Hold all the record in the table of group 3
        private List<Group3_Data_Model> Group3_Data_Model_list = new List<Group3_Data_Model>();

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

        private string GenerateUniqueKey()
        {
            Random random = new Random();
            int uniqueKey = random.Next(0, 65536);

            // Format the unique key as a hexadecimal string with leading zeros
            // X4 ensures it at least 4 characters with leading zeros
            string formattedKey = uniqueKey.ToString("X4");

            return formattedKey;
        }

        public Group3Repository(DateTime created)
        {
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
            _created = created;
            this.Created_Status = true;
            this.Connected_Status = false;
            this.Session_ID = GenerateUniqueKey();
        }

        public async Task<bool> Create(Group_Data_Model group_Data_Model, DateTime _Created)
        {
            if (!Created_Status || string.IsNullOrEmpty(connect_String) || string.IsNullOrEmpty(Session_ID))
            {
                return false;
            }

            try
            {
                _Group3_Data_Model = (Group3_Data_Model)group_Data_Model;
                using (MySqlConnection connection = new MySqlConnection(connect_String))
                {
                    await connection.OpenAsync();
                    string insertQuery = $"INSERT INTO {schemma}.{table_name} (sessionID, time, stock, sales, rating, pid, sid, clicks, clicked_time) " +
                        "VALUES (@sessionID, @time, @stock, @sales, @rating, @pid, @sid, @clicks, @clicked_time)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@sessionID", Session_ID);
                        cmd.Parameters.AddWithValue("@time", _Created);
                        cmd.Parameters.AddWithValue("@stock", _Group3_Data_Model.stock);
                        cmd.Parameters.AddWithValue("@sales", _Group3_Data_Model.sales);
                        cmd.Parameters.AddWithValue("@rating", _Group3_Data_Model.rating);
                        cmd.Parameters.AddWithValue("@pid", _Group3_Data_Model.pid);
                        cmd.Parameters.AddWithValue("@sid", _Group3_Data_Model.sid);
                        cmd.Parameters.AddWithValue("@clicks", _Group3_Data_Model.clicks);
                        cmd.Parameters.AddWithValue("@clicked_time", _Group3_Data_Model.clicked_time);

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public async Task<List<Group_Data_Model>> Read()
        {
            Group3_Data_Model_list = await ReadAll();
            return Group3_Data_Model_list.Cast<Group_Data_Model>().ToList();
        }

        private async Task<List<Group3_Data_Model>> ReadAll()
        {
            List<Group3_Data_Model> results = new List<Group3_Data_Model>();

            if (!Created_Status || string.IsNullOrEmpty(connect_String) || string.IsNullOrEmpty(Session_ID))
            {
                return results;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connect_String))
                {
                    await connection.OpenAsync();

                    // Define the SQL query to read data from the table
                    string selectQuery = $"SELECT * FROM {schemma}.{table_name} WHERE sessionID = @sessionID";

                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@sessionID", Session_ID);

                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Group3_Data_Model group3DataModel = new Group3_Data_Model
                                {
                                    stock = reader["stock"].ToString(),
                                    sales = reader["sales"].ToString(),
                                    rating = reader["rating"].ToString(),
                                    pid = reader["pid"].ToString(),
                                    sid = reader["sid"].ToString(),
                                    clicks = reader["clicks"].ToString(),
                                    clicked_time = reader["clicked_time"] is DBNull ? null : (DateTime?)reader["clicked_time"]
                                };

                                results.Add(group3DataModel);
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return results;
            }
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(Group_Data_Model group_Data_Model, DateTime _Created, string tablename)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, object>> ReadAllTables()
        {
            throw new NotImplementedException();
        }

        Task<object> IGroupRepository.ReadTable(string tablename)
        {
            throw new NotImplementedException();
        }
    }
}

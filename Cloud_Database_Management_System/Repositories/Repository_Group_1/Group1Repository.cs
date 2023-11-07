using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using MySqlConnector;

namespace Cloud_Database_Management_System.Repositories.Repository_Group_1
{
    public class Group1Repository : IGroupRepository
    {
        private Group1_Data_Model _Group1_Data_Model;
        private DateTime _created;
        private readonly string schemma = "analysis_and_reporting_raw_data";
        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";
        public bool Connected_Status { get; set; } = false;
        private bool Created_Status { get; set; } = false;
        // Hold all the record in the table of group 1
        private List<Group1_Data_Model> Group1_Data_Model_list = new List<Group1_Data_Model>();
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
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
            _created = created;
            Created_Status = true;
            Connected_Status = false;
            Session_ID = GenerateUniqueKey();
        }

        public async Task<bool> Create(Group_Data_Model group_Data_Model, DateTime _Created, string tablename)
        {// check for the tablename and Modify let table class do it
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connect_String))
                {
                    await connection.OpenAsync();

                    // Create a new SQL command to insert data into the group1_table
                    using (MySqlCommand cmd = new MySqlCommand($"INSERT INTO {tablename} (Column1, Column2, ...) VALUES (@Value1, @Value2, ...)", connection)) // Include time and sessionID later
                    {
                        //cmd.Parameters.AddWithValue("@Value1", group_Data_Model.Value1);
                        //cmd.Parameters.AddWithValue("@Value2", group_Data_Model.Value2);

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
        public Task<List<Group_Data_Model>> Read()
        {
            throw new NotImplementedException();
        }
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
            throw new NotImplementedException();
        }
    }
}

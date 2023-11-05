namespace Cloud_Database_Management_System.Models
{
    public class Database
    {
        public string Name { get; set; }

        public int DatabaseId { get; set; }

        public string DatabaseVersion { get; set; }

        public string DatabaseType { get; set; }

        public DateTime Created_Date { get; set; }

    }
}

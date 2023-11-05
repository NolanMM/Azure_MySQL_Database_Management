using Cloud_Database_Management_System.Models.Dto;

namespace Cloud_Database_Management_System.Data
{
    public static class DatabaseStore
    {
        public static List<DatabaseDTO> DatabaseList = new List<DatabaseDTO>
            {
                new DatabaseDTO{ Name = "Sample 1", DatabaseId = 1, DatabaseType = "Sample 1 Data Type", DatabaseVersion = "Sample 1 Data Version"},
                new DatabaseDTO{ Name = "Sample 2", DatabaseId = 2, DatabaseType = "Sample 2 Data Type", DatabaseVersion = "Sample 2 Data Version"},
                new DatabaseDTO{ Name = "Sample 3", DatabaseId = 3, DatabaseType = "Sample 3 Data Type", DatabaseVersion = "Sample 3 Data Version"},
                new DatabaseDTO{ Name = "Sample 4", DatabaseId = 4, DatabaseType = "Sample 4 Data Type", DatabaseVersion = "Sample 4 Data Version"},
                new DatabaseDTO{ Name = "Sample 5", DatabaseId = 5, DatabaseType = "Sample 5 Data Type", DatabaseVersion = "Sample 5 Data Version"}
            };
    }
}

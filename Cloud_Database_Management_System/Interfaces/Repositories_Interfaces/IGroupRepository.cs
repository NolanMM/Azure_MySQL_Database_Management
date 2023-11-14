using Cloud_Database_Management_System.Models.Group_Data_Models;

namespace Cloud_Database_Management_System.Interfaces.Repositories_Interfaces
{
    public interface IGroupRepository
    {
        Task<bool> Test_Connection_To_TableAsync();
        Task<bool> Create(Group_Data_Model group_Data_Model, DateTime _Created, string tablename);
        Task<object?> ReadTable(string tablename);
        Task<Dictionary<string, object>?> ReadAllTables(); // table name and list of records inside each table
        bool Update();
        bool Delete();
    }
}

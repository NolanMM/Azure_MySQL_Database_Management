using Cloud_Database_Management_System.Models.Group_Data_Models;

namespace Cloud_Database_Management_System.Interfaces.Repositories_Interfaces
{
    public interface IGroupRepository
    {
        bool Test_Connection_To_Table();
        Task<bool> Create(Group_Data_Model group_Data_Model, DateTime _Created, string tablename);
        Task<List<Group_Data_Model>> Read();
        bool Update();
        bool Delete();
    }
}

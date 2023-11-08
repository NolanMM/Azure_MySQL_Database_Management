using Cloud_Database_Management_System.Models.Group_Data_Models;

namespace Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface
{
    public interface Input_Tables_Template : Group_Data_Model
    {
        bool Test_Connection_To_Table();
        void Insert();
        void Delete();
        void Create();
        Task<List<object>?> ReadAllAsync();
        Task UpdateAsync();
    }
}

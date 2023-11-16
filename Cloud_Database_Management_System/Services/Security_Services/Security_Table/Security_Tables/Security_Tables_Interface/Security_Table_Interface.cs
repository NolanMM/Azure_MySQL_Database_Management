using Security_Services_Dev_Env.Services.Security_Services.Security_Table.Data_Models;

namespace Security_Services_Dev_Env.Services.Security_Services.Security_Table.Security_Tables.Security_Tables_Interface
{
    public interface Security_Table_Interface
    {
        Task<List<Security_Data_Model_Abtraction>?> ReadAllAsync_Security_Table();
        bool Test_Connection_To_Table();
        Task<bool> CreateAsync_Security_Table(Security_Data_Model_Abtraction dataModel, string tableName);
    }
}

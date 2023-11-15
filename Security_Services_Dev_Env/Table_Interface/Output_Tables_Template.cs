namespace Security_Services_Dev_Env.Repositories.Repository_Group_1.Table_Interface
{
    public interface Output_Tables_Template
    {
        bool Test_Connection_To_Table();
        Task<List<object>?> Read_All_Async();
        Task<List<object>?> Update_Async();
    }
}

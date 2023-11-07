namespace Cloud_Database_Management_System.Interfaces.Repositories_Interfaces
{
    public interface IGroupRepository
    {
        bool Test_Connection_To_Table();
        bool Create();
        bool Read();
        bool Update();
        bool Delete();
    }
}

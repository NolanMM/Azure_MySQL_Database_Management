namespace Cloud_Database_Management_System.Repositories.Repositories_Interfaces
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

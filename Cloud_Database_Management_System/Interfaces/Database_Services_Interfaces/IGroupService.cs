namespace Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces
{
    public interface IGroupService
    {
        bool TryProcessData(int groupId, object data, out object result);
    }
}
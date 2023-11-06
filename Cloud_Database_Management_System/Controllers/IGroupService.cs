namespace Cloud_Database_Management_System.Controllers
{
    public interface IGroupService
    {
        bool TryProcessData(int groupId, object data, out object result);
    }
}
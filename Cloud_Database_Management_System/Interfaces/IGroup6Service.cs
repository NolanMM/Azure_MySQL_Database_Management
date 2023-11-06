using Cloud_Database_Management_System.Models.Group_Data_Models;

namespace Cloud_Database_Management_System.Interfaces
{
    public interface IGroup6Service
    {
        bool TryProcessData(int groupId, object data, out Group6_Data_Model result);

    }
}

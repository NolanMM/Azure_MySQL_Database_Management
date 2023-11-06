using Cloud_Database_Management_System.Models.Group_Data_Models;

namespace Cloud_Database_Management_System.Interfaces
{
    public interface IGroup3Service
    {
        bool TryProcessData(int groupId, object data, out Group3_Data_Model result);

    }
}

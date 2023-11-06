using Cloud_Database_Management_System.Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group3Service : IGroupService
    {
        private readonly IGroup3Repository _group3Repository;

        public Group3Service(IGroup3Repository group3Repository)
        {
            _group3Repository = group3Repository;
        }
        public bool TryProcessData(int groupId, GroupData data, out ProcessedData result)
        {
            throw new NotImplementedException();
        }
    }
}

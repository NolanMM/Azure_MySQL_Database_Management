using Cloud_Database_Management_System.Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group2Service : IGroupService
    {
        private readonly IGroup2Repository _group2Repository;

        public Group2Service(IGroup2Repository group2Repository)
        {
            _group2Repository = group2Repository;
        }
        public bool TryProcessData(int groupId, GroupData data, out ProcessedData result)
        {
            throw new NotImplementedException();
        }
    }
}

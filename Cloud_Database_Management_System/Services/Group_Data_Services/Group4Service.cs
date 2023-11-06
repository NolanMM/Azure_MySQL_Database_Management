using Cloud_Database_Management_System.Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group4Service : IGroupService
    {
        private readonly IGroup4Repository _group4Repository;

        public Group4Service(IGroup4Repository group4Repository)
        {
            _group4Repository = group4Repository;
        }
        public bool TryProcessData(int groupId, GroupData data, out ProcessedData result)
        {
            throw new NotImplementedException();
        }
    }
}

using Cloud_Database_Management_System.Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group5Service : IGroupService
    {
        private readonly IGroup5Repository _group5Repository;

        public Group5Service(IGroup5Repository group5Repository)
        {
            _group5Repository = group5Repository;
        }
        public bool TryProcessData(int groupId, GroupData data, out ProcessedData result)
        {
            throw new NotImplementedException();
        }
    }
}

using Cloud_Database_Management_System.Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group6Service : IGroupService
    {
        private readonly IGroup6Repository _group6Repository;

        public Group6Service(IGroup6Repository group6Repository)
        {
            _group6Repository = group6Repository;
        }
        public bool TryProcessData(int groupId, GroupData data, out ProcessedData result)
        {
            throw new NotImplementedException();
        }
    }
}

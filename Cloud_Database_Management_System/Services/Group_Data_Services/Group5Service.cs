using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group5Service : IGroupService
    {
        private readonly IGroupRepository _group5Repository;

        public Group5Service(DateTime created)
        {
            _group5Repository = new Group5Repository(created);
        }
        public bool TryProcessData(int groupId, object data, out Group5_Data_Model result)
        {
            throw new NotImplementedException();
        }

        public bool TryProcessData(int groupId, object data, out object result)
        {
            throw new NotImplementedException();
        }
    }
}

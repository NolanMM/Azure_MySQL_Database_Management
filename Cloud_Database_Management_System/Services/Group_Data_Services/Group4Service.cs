using Cloud_Database_Management_System.Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;
using Cloud_Database_Management_System.Repositories.Repositories_Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group4Service : IGroup4Service
    {
        private readonly IGroupRepository _group4Repository;

        public Group4Service(DateTime created)
        {
            _group4Repository = new Group4Repository(created);
        }
        public bool TryProcessData(int groupId, object data, out Group4_Data_Model result)
        {
            throw new NotImplementedException();
        }

        public bool TryProcessData(int groupId, object data, out object result)
        {
            throw new NotImplementedException();
        }
    }
}

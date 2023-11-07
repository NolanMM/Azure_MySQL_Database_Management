using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public abstract class Group1Service : IGroupService
    {
        private readonly IGroupRepository _group1Repository;

        public Group1Service(DateTime created)
        {
             _group1Repository = new Group1Repository(created);
        }
        public bool TryProcessData(int groupId, object data, out Group1_Data_Model result)
        {
            throw new NotImplementedException();
        }

        public bool TryProcessData(int groupId, object data, out object result)
        {
            throw new NotImplementedException();
        }
    }
}

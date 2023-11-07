using Cloud_Database_Management_System.Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;
using Cloud_Database_Management_System.Repositories.Repositories_Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group3Service : IGroup3Service
    {
        private readonly IGroupRepository _group3Repository;

        public Group3Service(DateTime created)
        {
            _group3Repository = new Group3Repository(created);
        }
        public bool TryProcessData(int groupId, object data, out Group3_Data_Model result)
        {
            throw new NotImplementedException();
        }

        public bool TryProcessData(int groupId, object data, out object result)
        {
            throw new NotImplementedException();
        }
    }
}

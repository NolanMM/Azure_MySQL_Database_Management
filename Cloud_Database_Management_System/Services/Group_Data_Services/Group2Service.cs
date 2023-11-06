using Cloud_Database_Management_System.Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories.Repositories_Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group2Service : IGroup2Service
    {
        private readonly IGroup2Repository _group2Repository;

        public Group2Service(IGroup2Repository group2Repository)
        {
            _group2Repository = group2Repository;
        }
        public bool TryProcessData(int groupId, object data, out Group2_Data_Model result)
        {
            throw new NotImplementedException();
        }

        public bool TryProcessData(int groupId, object data, out object result)
        {
            throw new NotImplementedException();
        }
    }
}

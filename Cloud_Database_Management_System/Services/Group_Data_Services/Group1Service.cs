using Cloud_Database_Management_System.Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories.Repositories_Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group1Service : IGroup1Service
    {
        private readonly IGroup1Repository _group1Repository;

        public Group1Service(IGroup1Repository group1Repository)
        {
            _group1Repository = group1Repository;
        }
        public bool TryProcessData(int groupId, object data, out Group1_Data_Model result)
        {
            throw new NotImplementedException();
        }
    }
}

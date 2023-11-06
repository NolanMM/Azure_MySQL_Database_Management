using Cloud_Database_Management_System.Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories.Repositories_Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group5Service : IGroup5Service
    {
        private readonly IGroup5Repository _group5Repository;

        public Group5Service(IGroup5Repository group5Repository)
        {
            _group5Repository = group5Repository;
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

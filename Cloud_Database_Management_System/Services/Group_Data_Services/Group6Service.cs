using Cloud_Database_Management_System.Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories.Repositories_Interfaces;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group6Service : IGroup6Service
    {
        private readonly IGroup6Repository _group6Repository;

        public Group6Service(IGroup6Repository group6Repository)
        {
            _group6Repository = group6Repository;
        }
        public bool TryProcessData(int groupId, object data, out Group6_Data_Model result)
        {
            throw new NotImplementedException();
        }

        public bool TryProcessData(int groupId, object data, out object result)
        {
            throw new NotImplementedException();
        }
    }
}

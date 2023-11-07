using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group6Service : IGroupService
    {
        private readonly IGroupRepository _group6Repository;

        public Group6Service(DateTime created)
        {
            _group6Repository = new Group6Repository(created);
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

using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;
using System.Text.Json;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group5Service : IGroupService
    {
        private IGroupRepository _Group5Repository;
        private Group5_Data_Model _Group5_Data_Model;
        private DateTime _Created;

        public Group5Service(DateTime created, object data)
        {
            _Group5Repository = new Group5Repository(created);
            _Created = created;
        }
        public Group5Service(DateTime created)
        {
            _Group5Repository = new Group5Repository(created);
            _Created = created;
        }

        public object ProcessGetRequestDataCorrespondGroupID(int tablenumber)
        {
            throw new NotImplementedException();
        }

        public bool ProcessPostRequestDataCorrespondGroupID(object data, int Tablenumber)
        {
            try
            {
                _Group5_Data_Model = ProcessDataForGroup5(data);
                if (_Group5_Data_Model != null)
                {
                    _Group5Repository.Create(_Group5_Data_Model, _Created, Tablenumber.ToString());
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Group5_Data_Model? ProcessDataForGroup5(object data)
        {
            if (data == null) { return null; }
            try
            {
                return JsonSerializer.Deserialize<Group5_Data_Model>(data.ToString());
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public object ProcessGetRequestAllDataTablesCorrespondGroupID()
        {
            throw new NotImplementedException();
        }

        Task<object> IGroupService.ProcessGetRequestAllDataTablesCorrespondGroupID()
        {
            throw new NotImplementedException();
        }
    }
}

using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;
using System.Text.Json;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group2Service : IGroupService
    {
        private IGroupRepository _Group2Repository;
        private Group2_Data_Model _Group2_DataModel;
        private DateTime _Created;

        public Group2Service(DateTime created, object data)
        {
            _Group2Repository = new Group2Repository(created);
            _Created = created;
        }
        public Group2Service(DateTime created)
        {
            _Group2Repository = new Group2Repository(created);
            _Created = created;
        }
        public async Task<object> ProcessGetRequestDataCorrespondGroupID(int tablenumber)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ProcessPostRequestDataCorrespondGroupIDAsync(object data, int Tablenumber)
        {
            try
            {
                _Group2_DataModel = ProcessDataForGroup2(data);
                if (_Group2_DataModel != null)
                {
                    await _Group2Repository.Create(_Group2_DataModel, _Created, Tablenumber.ToString());
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Group2_Data_Model? ProcessDataForGroup2(object data)
        {
            if (data == null) { return null; }
            try
            {
                return JsonSerializer.Deserialize<Group2_Data_Model>(data.ToString());
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

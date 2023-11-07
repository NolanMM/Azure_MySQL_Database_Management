using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;
using System.Text.Json;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group3Service : IGroupService
    {
        private IGroupRepository _Group3Repository;
        private Group3_Data_Model _Group3_DataModel;
        private DateTime _Created;

        public Group3Service(DateTime created, object data)
        {
            _Group3Repository = new Group3Repository(created);
            _Created = created;
        }

        public bool ProcessGetRequestDataCorrespondGroupID(object data, string Tablename)
        {
            throw new NotImplementedException();
        }

        public bool ProcessPostRequestDataCorrespondGroupID(object data, string Tablename)
        {
            try
            {
                _Group3_DataModel = ProcessDataForGroup3(data);
                if (_Group3_DataModel != null)
                {
                    _Group3Repository.Create(_Group3_DataModel, _Created, Tablename);
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Group3_Data_Model? ProcessDataForGroup3(object data)
        {
            if (data == null) { return null; }
            try
            {
                return JsonSerializer.Deserialize<Group3_Data_Model>(data.ToString());
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}

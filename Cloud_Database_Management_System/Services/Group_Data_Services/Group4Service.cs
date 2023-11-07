using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;
using System.Text.Json;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group4Service : IGroupService
    {
        private IGroupRepository _Group4Repository;
        private Group4_Data_Model _Group4DataModel;
        private DateTime _Created;

        public Group4Service(DateTime created, object data)
        {
            _Group4Repository = new Group4Repository(created);
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
                _Group4DataModel = ProcessDataForGroup4(data);
                if (_Group4DataModel != null)
                {
                    _Group4Repository.Create(_Group4DataModel, _Created, Tablename);
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Group4_Data_Model? ProcessDataForGroup4(object data)
        {
            if (data == null) { return null; }
            try
            {
                return JsonSerializer.Deserialize<Group4_Data_Model>(data.ToString());
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}

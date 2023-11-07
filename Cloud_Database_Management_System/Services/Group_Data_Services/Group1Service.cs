using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories.Repository_Group_1;
using System.Text.Json;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group1Service : IGroupService
    {
        private IGroupRepository _Group1Repository;
        private Group1_Data_Model _Group1_Data_Model;
        private DateTime _Created;

        public Group1Service(DateTime created, object data)
        {
            _Group1Repository = new Group1Repository(created);
            _Created = created;
        }

        public bool ProcessGetRequestDataCorrespondGroupID(object data, string Tablename)
        {
            throw new NotImplementedException();
        }

        public bool ProcessPostRequestDataCorrespondGroupID(object data,string tablename)
        {
            try
            {
                _Group1_Data_Model = ProcessDataForGroup1(data);
                if (_Group1_Data_Model != null)
                {
                    _Group1Repository.Create(_Group1_Data_Model, _Created, tablename);
                    return true;
                }else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Group1_Data_Model? ProcessDataForGroup1(object data)
        {
            if (data == null) { return null; }
            try
            {
                return JsonSerializer.Deserialize<Group1_Data_Model>(data.ToString());
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}

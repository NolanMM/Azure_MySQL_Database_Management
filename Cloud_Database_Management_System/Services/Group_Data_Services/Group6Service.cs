using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Interfaces.Repositories_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Repositories;
using System.Text.Json;

namespace Cloud_Database_Management_System.Services.Group_Data_Services
{
    public class Group6Service : IGroupService
    {
        private IGroupRepository _Group6Repository;
        private Group6_Data_Model _Group6_DataModel;
        private DateTime _Created;

        public Group6Service(DateTime created, object data)
        {
            _Group6Repository = new Group6Repository(created);
            _Created = created;
        }

        public Group6Service(DateTime created)
        {
            _Group6Repository = new Group6Repository(created);
            _Created = created;
        }

        public bool ProcessGetRequestDataCorrespondGroupID(int tablenumber)
        {
            throw new NotImplementedException();
        }

        public bool ProcessPostRequestDataCorrespondGroupID(object data, int Tablenumber)
        {
            try
            {
                _Group6_DataModel = ProcessDataForGroup6(data);
                if (_Group6_DataModel != null)
                {
                    _Group6Repository.Create(_Group6_DataModel, _Created, Tablenumber.ToString());
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private Group6_Data_Model? ProcessDataForGroup6(object data)
        {
            if (data == null) { return null; }
            try
            {
                return JsonSerializer.Deserialize<Group6_Data_Model>(data.ToString());
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}

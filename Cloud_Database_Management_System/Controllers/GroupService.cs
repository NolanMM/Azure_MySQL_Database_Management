using Cloud_Database_Management_System.Models.Group_Data_Models;
using System.Text.Json;
using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Services.Group_Data_Services;

namespace Cloud_Database_Management_System.Controllers
{
    public class GroupService
    {
        private IGroupService groupService;
        private DateTime _created;
        
        public GroupService(DateTime created)
        {
            _created = created;
        }

        public bool ProcessPostData(int groupId, string Tablename, object data)
        {
            if (groupId == 0) { return false; }
            else {
                switch (groupId)
                {
                    case 1:
                        var Group_1_Services = new Group1Service(_created, data);
                        if (Group_1_Services == null)
                        {
                            return false;
                        }else
                        {
                            groupService = Group_1_Services;
                            groupService.ProcessPostRequestDataCorrespondGroupID(data, Tablename);
                            return true;
                        }
                    case 2:
                        var Group_2_Services = new Group2Service(_created, data);
                        if (Group_2_Services == null)
                        {
                            return false;
                        }
                        else
                        {
                            groupService = Group_2_Services;
                            groupService.ProcessPostRequestDataCorrespondGroupID(data, Tablename);
                            return true;
                        }
                    case 3:
                        var Group_3_Services = new Group3Service(_created, data);
                        if (Group_3_Services == null)
                        {
                            return false;
                        }
                        else
                        {
                            groupService = Group_3_Services;
                            groupService.ProcessPostRequestDataCorrespondGroupID(data, Tablename);
                            return true;
                        }
                    case 4:
                        var Group_4_Services = new Group4Service(_created, data);
                        if (Group_4_Services == null)
                        {
                            return false;
                        }
                        else
                        {
                            groupService = Group_4_Services;
                            groupService.ProcessPostRequestDataCorrespondGroupID(data, Tablename);
                            return true;
                        }
                    case 5:
                        var Group_5_Services = new Group5Service(_created, data);
                        if (Group_5_Services == null)
                        {
                            return false;
                        }
                        else
                        {
                            groupService = Group_5_Services;
                            groupService.ProcessPostRequestDataCorrespondGroupID(data, Tablename);
                            return true;
                        }
                    case 6:
                        var Group_6_Services = new Group6Service(_created, data);
                        if (Group_6_Services == null)
                        {
                            return false;
                        }
                        else
                        {
                            groupService = Group_6_Services;
                            groupService.ProcessPostRequestDataCorrespondGroupID(data, Tablename);
                            return true;
                        }
                    default:
                        return false;
                }
            }
        }
        public bool ProcessGetData(int groupId, string Tablename, object data, out object result)
        {
            result = null;

            if (groupId == 0)
            {
                return false;
            }
            else
            {
                switch (groupId)
                {
                    case 1:
                        var Group_1_Services = new Group1Service(_created, data);
                        if (Group_1_Services == null)
                        {
                            return false;
                        }
                        else
                        {
                            groupService = Group_1_Services;
                            groupService.ProcessGetRequestDataCorrespondGroupID(data, Tablename);
                            result = groupService; // Set the result to the groupService instance
                            return true;
                        }
                    default:
                        return false;
                }
            }
        }
    }
}

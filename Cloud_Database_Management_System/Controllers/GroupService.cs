using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Cloud_Database_Management_System.Services.Group_Data_Services;
using Microsoft.AspNetCore.Http;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;
using System.Text.Json;

namespace Cloud_Database_Management_System.Controllers
{
    public class GroupService
    {
        private IGroupService groupService;
        private DateTime _created { get; set; }
        
        public GroupService(DateTime created)
        {
            _created = DateTime.Now;
        }

        public async Task<bool> ProcessPostDataAsync(int groupId, int TableNumber, object data)
        {
            if (groupId == 0) { return false; }
            else {
                switch (groupId)
                {
                    case 1:
                        _created = DateTime.Now;
                        var Group_1_Services = new Group1Service(_created, data);
                        if (Group_1_Services == null)
                        {
                            return false;
                        }else
                        {
                            groupService = Group_1_Services;
                            try
                            {
                                bool result = await groupService.ProcessPostRequestDataCorrespondGroupIDAsync(data, TableNumber);

                                if (!result)
                                {
                                    return false;
                                }

                                return true;
                            }
                            catch (Exception ex)
                            {
                                string dataString = JsonSerializer.Serialize(data);
                                string request_type = "POSTGroupServicesError";
                                string Issues = ex.Message;
                                string Request_Status = "Failed";
                                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                                        request_type,
                                        DateTime.Now,
                                        TableNumber.ToString(),
                                        dataString,
                                        Request_Status,
                                        Issues
                                    );
                                if (logStatus)
                                {
                                    Console.WriteLine("Error: " + ex.Message);
                                    return false;
                                }
                                else
                                {
                                    Console.WriteLine("Error: " + ex.Message);
                                    return false;
                                }
                            }
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
                            await groupService.ProcessPostRequestDataCorrespondGroupIDAsync(data, TableNumber);
                            return true;
                        }
                    default:
                        return false;
                }
            }
        }
        public async Task<object> ProcessGetData(int groupId, int tableNumber)
        {
            object result = null;

            if (groupId == 0)
            {
                return false;
            }
            else
            {
                switch (groupId)
                {
                    case 1:
                        var Group_1_Services = new Group1Service(_created);
                        if (Group_1_Services == null)
                        {
                            return false;
                        }
                        else
                        {
                            groupService = Group_1_Services;
                            var serviceResult = await groupService.ProcessGetRequestDataCorrespondGroupID(tableNumber);

                            if (serviceResult is object)
                            {
                                result = serviceResult;
                                return result;
                            }
                            else if (serviceResult is string)
                            {
                                result = serviceResult;
                                return result;
                            }
                        }
                        return false;
                    default:
                        return false;
                }
            }
        }
        public async Task<object> ProcessGetAllData(int groupId)
        {
            object result = null;

            if (groupId == 0)
            {
                return false;
            }
            else
            {
                switch (groupId)
                {
                    case 1:
                         groupService = new Group1Service(_created);
                        if (groupService != null)
                        {
                            var serviceResult = await groupService.ProcessGetRequestAllDataTablesCorrespondGroupID();

                            if (serviceResult is Dictionary<string, object>)
                            {
                                result = serviceResult;
                                return result;
                            }
                            else if (serviceResult is Exception)
                            {
                                result = serviceResult;
                                return result;
                            }
                        }
                        return false;
                    default:
                        return false;
                }
            }
        }
    }
}

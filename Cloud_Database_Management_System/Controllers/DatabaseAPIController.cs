using Microsoft.AspNetCore.Mvc;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;
using System.Text.Json;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;
using System.Text.Json;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;

namespace Cloud_Database_Management_System.Controllers
{
    [Route("Group1/DatabaseController")]
    [ApiController]
    public class DatabaseAPIController : ControllerBase
    {
        private GroupService _groupService;

        public DatabaseAPIController()
        {
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessPostDataAsync(int groupId, int tableNumber, [FromBody] object data)
        {
            string requestType = "POST";
            string dataString = JsonSerializer.Serialize(data);

            // Input validation
            if (groupId == 1 && 0 <= tableNumber && tableNumber <= Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count())
            {
                try
                {
                    object result = await _groupService.ProcessPostDataAsync(groupId, tableNumber, data);
                    Table_Group_1_Dictionary? tableInfo = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.FirstOrDefault(info => info.Index == tableNumber);

                    if (result is bool && !(bool)result)
                    {
                        return BadRequest($"Error {requestType} To Table: {tableInfo?.TableName}");
                    }

                    return Ok(true);
                }
                catch (Exception ex)
                {
                    string issues = ex.Message;
                    string requestStatus = "Failed";
                    string Issues = ex.Message;
                    return await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);
                }
            }
            else if (groupId == 1 && (tableNumber < 0 || tableNumber > Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count()))
            {
                string issues = "Cannot find the table in the group. Please try again";
                string requestStatus = "Failed";

                return await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);
            }
            else if (groupId != 1 && 0 <= tableNumber && tableNumber <= Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count())
            {
                string issues = "Cannot find the group. Please try again";
                string requestStatus = "Failed";

                return await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);
            }
            else
            {
                string issues = "Someone Try to attack, Server in danger!!!!";
                string requestStatus = "Failed";
                requestType = "AttackPost";

                return await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);
            }
        }

                        tableNumber.ToString(),
                        dataString,
                        Request_Status,
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                    );
                if (logStatus)
            string requestType = "GET_By_Group_ID_Table_Number";

            // Input validation
            if (groupId != 1 || tableNumber < 0 || tableNumber > Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count())
            {
                return BadRequest("Invalid group ID or table number");
            }

            try
            {
                object result = await _groupService.ProcessGetData(groupId, tableNumber);
                }
                if (result is bool && !(bool)result)
                {
                    return BadRequest("Invalid data for the specified group.");
                }
                else if (result is object || result is string)
                {
                    return Ok(result);
                }
                    );
                return NotFound();
            }
            catch (Exception ex)
            {
                string dataString = $"GET group ID: {groupId}, table number: {tableNumber}";
                string requestStatus = "Failed";
                string issues = $"An error occurred: {ex.Message}";

                await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);

                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
                request_type = "AttackPost";
                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                    request_type,
                    DateTime.Now,
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
                    dataString,
                    Request_Status,
            string requestType = "GET_All_Data_Group_1";

            // Input validation
            if (groupId != 1)
            {
                string issues = "Invalid group ID";
                string dataString = $"GET group ID: {groupId} All Tables";
                string requestStatus = "Failed";

                return await LogError(requestType, "HACK All Tables", dataString, requestStatus, issues);
            }

            try
            {
                var result = await _groupService.ProcessGetAllData(groupId);
                else
                if (result is bool && !(bool)result)
                {
                    return BadRequest("Invalid data for the specified group.");
                }
                else if (result is Dictionary<string, object>)
                {
                    return Ok(result);
                }
                else if (result is Exception)
                {
                    return BadRequest($"An error occurred: {((Exception)result).Message}");
                }
            string request_type = "GET_By_Group_ID_Table_Number";
                return NotFound();
            }
            catch (Exception ex)
            {
                string dataString = $"GET group ID: {groupId} All Tables";
                string issues = $"An error occurred: {ex.Message}";
                string requestStatus = "Failed";

                return await LogError(requestType, "Get_All_Tables", dataString, requestStatus, issues);
            }
        }

        private async Task<IActionResult> LogError(string requestType, string tableNumber, string dataString, string requestStatus, string issues)
        {
            bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                requestType,
                DateTime.Now,
                tableNumber,
                dataString,
                requestStatus,
                issues
            );

            if (logStatus)
            {
                Console.WriteLine($"Error: {issues}");
                return NotFound("This server does not exist");
            }
            else
            {
                Console.WriteLine($"Error: {issues}");
                return NotFound("Cannot log the error. It's broke!");
            }
        }
                    if (logStatus)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
                    }
                    else
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
                    }
                }
            }
            else if (groupId == 1 && (tableNumber < 0 || tableNumber > Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count()))
            {
                string Issues = "Cannot find the table in the group. Please try again";
                string Request_Status = "Failed";
                string dataString = "GET group ID: " + groupId + " , table number: " + tableNumber;
                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                        request_type,
                        DateTime.Now,
                        tableNumber.ToString(),
                        dataString,
                        Request_Status,
                        Issues
                    );
                if (logStatus)
                {
                    return NotFound(Issues);
                }
                else
                {
                    return NotFound("Cannot log the \"Cannot find the table in the group. Please try again.\". Its broke!!!!");
                }
            }
            else if (groupId != 1 && 0 <= tableNumber && tableNumber <= Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count())
            {
                string Issues = "Cannot find the group. Please try again";
                string dataString = "GET group ID: " + groupId + " , table number: " + tableNumber;
                string Request_Status = "Failed";
                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                        request_type,
                        DateTime.Now,
                        tableNumber.ToString(),
                        dataString,
                        Request_Status,
                        Issues
                    );
                if (logStatus)
                {
                    return NotFound(Issues);
                }
                else
                {
                    return NotFound("Cannot log the \"Cannot find the group.Please try again\". Its broke!!!!!");
                }
            }
            else
            {
                string Issues = "Someone Try to attack, Server in danger!!!!";
                string dataString = "GET group ID: " + groupId + " , table number: " + tableNumber;
                string Request_Status = "Failed";
                request_type = "AttackPost";
                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                    request_type,
                    DateTime.Now,
                    tableNumber.ToString(),
                    dataString,
                    Request_Status,
                    Issues
                );
                if (logStatus)
                {
                    return NotFound("This server does not exist");
                }
                else
                {
                    return NotFound("Cannot log the \"Cannot find the group.Please try again\". Its broke!!!!!");
                }
            }
        }

        [HttpGet("group{groupId}/GetAllData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProcessGetAllDataTable(int groupId)
        {
            string request_type = "GET_All_Data_Group_1";
            if (groupId == 1)
            {
                try
                {
                    var result = await _groupService.ProcessGetAllData(groupId);

                    if (result is bool && !(bool)result)
                    {
                        return BadRequest("Invalid data for the specified group.");
                    }
                    else if (result is Dictionary<string, object>)
                    {
                        return Ok(result);
                    }
                    else if (result is Exception)
                    {
                        return BadRequest($"An error occurred: {((Exception)result).Message}");
                    }

                    return NotFound();
                }
                catch (Exception ex)
                {
                    string dataString = "GET group ID: " + groupId + " All Tables";
                    string Issues = $"An error occurred: {ex.Message}";
                    string Request_Status = "Failed";
                    bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                            request_type,
                            DateTime.Now,
                            "Get_All_Tables",
                            dataString,
                            Request_Status,
                            Issues
                        );
                    if (logStatus)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
                    }
                    else
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
                    }

                }
            }
            else
            {
                string Issues = "Someone Try to attack, Server in danger!!!!";
                string dataString = "GET group ID: " + groupId + " All Tables";
                string Request_Status = "Failed";
                request_type = "AttackPost";
                bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                    request_type,
                    DateTime.Now,
                    "HACK All Tables",
                    dataString,
                    Request_Status,
                    Issues
                );
                if (logStatus)
                {
                    return NotFound("This server does not exist");
                }
                else
                {
                    return NotFound("Cannot log the \"Cannot find the group.Please try again\". Its broke!!!!!");
                }
            }
        }
    }
}

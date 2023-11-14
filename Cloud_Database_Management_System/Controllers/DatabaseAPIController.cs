using Microsoft.AspNetCore.Mvc;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;
using System.Text.Json;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;

namespace Cloud_Database_Management_System.Controllers
{
    [Route("Group1/DatabaseController")]
    [ApiController]
    public class DatabaseAPIController : ControllerBase
    {
        private readonly GroupService _groupService;

        public DatabaseAPIController(GroupService groupService)
        {
            _groupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        [HttpPost("POST/group{groupId}/{tableNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [HttpGet("group{groupId}/{tableNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessGetData(int groupId, int tableNumber)
        {
            string requestType = "GET_By_Group_ID_Table_Number";

            // Input validation
            if (groupId != 1 || tableNumber < 0 || tableNumber > Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count())
            {
                return BadRequest("Invalid group ID or table number");
            }

            try
            {
                object result = await _groupService.ProcessGetData(groupId, tableNumber);

                if (result is bool && !(bool)result)
                {
                    return BadRequest("Invalid data for the specified group.");
                }
                else if (result is object || result is string)
                {
                    return Ok(result);
                }

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

        [HttpGet("group{groupId}/GetAllData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessGetAllDataTable(int groupId)
        {
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
    }
}
using Microsoft.AspNetCore.Mvc;
using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;

namespace Cloud_Database_Management_System.Controllers
{
    [Route("Group1/DatabaseController")]
    [ApiController]
    public class DatabaseAPIController : ControllerBase
    {
        private GroupService _groupService;

        public DatabaseAPIController()
        {
            _groupService = new GroupService(DateTime.Now);
        }

        [HttpPost("group{groupId}/{TableNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessPostDataAsync(int groupId, int TableNumber,  [FromBody] object data)
        {
            object results = await _groupService.ProcessPostDataAsync(groupId, TableNumber, data);
            if (results is bool && !(bool)results)
            {
                return BadRequest("Invalid data for the specified group.");
            }

            return Ok(true);
        }

        [HttpGet("group{groupId}/{tableNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessGetData(int groupId, int tableNumber)
        {
            object result = await _groupService.ProcessGetData(groupId, tableNumber);

            if (result is bool && !(bool)result)
            {
                return BadRequest("Invalid data for the specified group.");
            }
            else if (result is object)
            {
                return Ok(result);
            }
            else if (result is string)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("group{groupId}/GetAllData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessGetAllDataTable(int groupId)
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
                return BadRequest("An error occurred: " + ((Exception)result).Message);
            }

            return NotFound();
        }
    }
}

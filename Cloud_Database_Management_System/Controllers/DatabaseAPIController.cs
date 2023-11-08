using Microsoft.AspNetCore.Mvc;
using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;

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
        public IActionResult ProcessGroupData(int groupId, int TableNumber,  [FromBody] object data)
        {
            if (!_groupService.ProcessPostData(groupId, TableNumber, data))
            {
                return BadRequest("Invalid data for the specified group.");
            }

            return Ok(true);
        }

        [HttpGet("group{groupId}/{TableNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ProcessGroupDa1ta(int groupId, int TableNumber)
        {
            if (!_groupService.ProcessGetData(groupId, TableNumber, out var result))
            {
                return BadRequest("Invalid data for the specified group.");
            }

            return Ok(result);
        }

    }
}

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

        // Create New record base on group ID and table name
        [HttpPost("group{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ProcessGroupData(int groupId, string Tablename,  [FromBody] object data)
        {
            if (!_groupService.ProcessPostData(groupId, Tablename, data))
            {
                return BadRequest("Invalid data for the specified group.");
            }

            return Ok(true);
        }


        [HttpGet("group{groupId}/{Tablename}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ProcessGroupDa1ta(int groupId, string Tablename, [FromBody] object data)
        {
            if (!_groupService.ProcessGetData(groupId, Tablename, data, out var result))
            {
                return BadRequest("Invalid data for the specified group.");
            }

            return Ok(result);
        }

    }
}

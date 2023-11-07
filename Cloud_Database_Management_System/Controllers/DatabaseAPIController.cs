using Microsoft.AspNetCore.Mvc;
using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;

namespace Cloud_Database_Management_System.Controllers
{
    [Route("Group1/DatabaseController")]
    //[ApiController]
    public class DatabaseAPIController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public DatabaseAPIController()
        {
            _groupService = new GroupService(DateTime.Now);
        }

        [HttpPost("group{groupId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ProcessGroupData(int groupId, [FromBody] object data)
        {
            if (!_groupService.TryProcessData(groupId, data, out var result))
            {
                return BadRequest("Invalid data for the specified group.");
            }

            return Ok(result);
        }

    }
}

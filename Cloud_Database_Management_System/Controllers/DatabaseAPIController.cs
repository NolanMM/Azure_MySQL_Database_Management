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
        public IActionResult ProcessPostData(int groupId, int TableNumber,  [FromBody] object data)
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
        public IActionResult ProcessGetData(int groupId, int TableNumber)
        {
            if (!_groupService.ProcessGetData(groupId, TableNumber, out var result))
            {
                return BadRequest("Invalid data for the specified group.");
            }

            return Ok(result);
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

        //[HttpGet("group{groupId}/GetAllData")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public IActionResult CheckTheConnection(int groupId)
        //{
        //    if (!_groupService.ProcessGetAllData(groupId, out var result))
        //    {
        //        return BadRequest("Invalid data for the specified group.");
        //    }

        //    if (result is Dictionary<string, object>)
        //    {
        //        return Ok(result);
        //    }
        //    else if (result is Exception)
        //    {
        //        // Handle the exception if needed
        //        return BadRequest("An error occurred: " + ((Exception)result).Message);
        //    }

        //    return NotFound();
        //}

    }
}

using Cloud_Database_Management_System.Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Cloud_Database_Management_System.Controllers
{
    [Route("Group1/DatabaseController")]
    //[ApiController]
    public class DatabaseAPIController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public DatabaseAPIController(IGroupService groupService)
        {
            _groupService = groupService;
        }


        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<IEnumerable<DatabaseDTO>> GetDatabases()
        //{
        //    return Ok(DatabaseStore.DatabaseList);
        //}

        //[HttpGet("{Name_Identify}/{id:int}", Name = "GetDatabase")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<DatabaseDTO> GetDatabases(string Name_Identify, [FromBody] object dataObject, int id)
        //{
        //    if (id == 0)
        //    {
        //        return BadRequest();
        //    }

        //    Dictionary<string, Type> dataClassMap = new Dictionary<string, Type>
        //    {
        //        { "Group1", typeof(Group1_Data_Model) },
        //        { "Group2", typeof(Group2_Data_Model) },
        //        { "Group3", typeof(Group3_Data_Model) },
        //        { "Group4", typeof(Group4_Data_Model) },
        //        { "Group5", typeof(Group5_Data_Model) },
        //        { "Group6", typeof(Group6_Data_Model) }
        //    };

        //    if (dataClassMap.TryGetValue(Name_Identify, out Type dataClassType))
        //    {
        //        // Use System.Text.Json to deserialize the dataObject into the corresponding data class
        //        try
        //        {
        //            var data = JsonSerializer.Deserialize(JsonSerializer.Serialize(dataObject), dataClassType);
        //            return Ok(data);
        //        }
        //        catch (JsonException)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpPost("group{groupId}")]
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

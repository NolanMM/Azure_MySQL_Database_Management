using Cloud_Database_Management_System.Data;
using Cloud_Database_Management_System.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Cloud_Database_Management_System.Controllers
{
    [Route("Group1/DatabaseController")]
    //[ApiController]
    public class DatabaseAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<DatabaseDTO>> GetDatabases()
        {
            return Ok(DatabaseStore.DatabaseList);
        }

        [HttpGet("{id:int}", Name = "GetDatabase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // Customs response codes so no need to put type in the ActionResult<DatabaseDTO> just return ActionResult
        //[ProducesResponseType(200, Type = typeof(DatabaseDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public ActionResult<DatabaseDTO> GetDatabases(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var database = DatabaseStore.DatabaseList.FirstOrDefault(u => u.DatabaseId == id);
            if(database == null)
            {
                return NotFound();
            }
            return Ok(database);
        }

        // Using [ApiController] Attribute
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<DatabaseDTO> CreateDatabaseQuery([FromBody]DatabaseDTO database)
        //{
        //    if(database == null)
        //    {
        //        return BadRequest(database);
        //    }
        //    if(database.DatabaseId > 0)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //    //database.DatabaseId = DatabaseStore.DatabaseList[database.DatabaseId].DatabaseId;
        //    database.DatabaseId = DatabaseStore.DatabaseList.OrderByDescending(u => u.DatabaseId).FirstOrDefault().DatabaseId + 1;
        //    DatabaseStore.DatabaseList.Add(database);
        //    //return Ok(database);

        //    return CreatedAtRoute("GetDatabase", new { id = database.DatabaseId } ,database);
        //}

        // No using [ApiController] Attribute on top
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DatabaseDTO> CreateDatabaseQuery([FromBody] DatabaseDTO database)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);          // Return error message
            //}
            if(DatabaseStore.DatabaseList.FirstOrDefault(u => u.Name.ToLower()  == database.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Query Error", "Database already exist!");
                return BadRequest(ModelState);
            }
            if (database == null)
            {
                return BadRequest(database);
            }
            if (database.DatabaseId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //database.DatabaseId = DatabaseStore.DatabaseList[database.DatabaseId].DatabaseId;
            database.DatabaseId = DatabaseStore.DatabaseList.OrderByDescending(u => u.DatabaseId).FirstOrDefault().DatabaseId + 1;
            DatabaseStore.DatabaseList.Add(database);
            //return Ok(database);

            return CreatedAtRoute("GetDatabase", new { id = database.DatabaseId }, database);
        }

        [HttpDelete("{id:int}", Name = "DeleteDatabase")]

        public IActionResult DeleteDatabase(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var database = DatabaseStore.DatabaseList.FirstOrDefault(u => u.DatabaseId == id);
            if (database == null)
            {
                return NotFound();
            }
            DatabaseStore.DatabaseList.Remove(database);
            return NoContent();
        }

    }
}

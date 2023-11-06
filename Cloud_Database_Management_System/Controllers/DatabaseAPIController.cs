using Cloud_Database_Management_System.Data;
using Cloud_Database_Management_System.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        [HttpGet("{Name_Identify}/{id:int}", Name = "GetDatabase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DatabaseDTO> GetDatabases(string Name_Identify, [FromBody] object dataObject, int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            Dictionary<string, Type> dataClassMap = new Dictionary<string, Type>
            {
                { "Group1", typeof(Group1DataClass) },
                { "Group2", typeof(Group2DataClass) },
                { "Group3", typeof(Group3DataClass) },
                { "Group4", typeof(Group4DataClass) },
                { "Group5", typeof(Group5DataClass) },
                { "Group6", typeof(Group6DataClass) }
            };

            if (dataClassMap.TryGetValue(Name_Identify, out Type dataClassType))
            {
                // Use System.Text.Json to deserialize the dataObject into the corresponding data class
                try
                {
                    var data = JsonSerializer.Deserialize(JsonSerializer.Serialize(dataObject), dataClassType);
                    return Ok(data);
                }
                catch (JsonException)
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
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

        [HttpPut("{id:int}", Name = "UpdateDatabase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]DatabaseDTO database)
        {
            if (database == null || id != database.DatabaseId)
            {
                return BadRequest(database);
            }
            var database_ = DatabaseStore.DatabaseList.FirstOrDefault(u => u.DatabaseId == id);
            database_.Name = database.Name;
            database_.DatabaseId = database.DatabaseId;
            database_.DatabaseVersion = database.DatabaseVersion;
            database_.DatabaseType = database.DatabaseType;

            return NoContent();
        }

    }
}

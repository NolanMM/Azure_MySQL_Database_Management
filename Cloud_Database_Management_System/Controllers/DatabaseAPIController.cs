using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Cloud_Database_Management_System.Interfaces.Database_Services_Interfaces;
using Cloud_Database_Management_System.Models.Group_Data_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ProcessPostDataAsync(int groupId, int tableNumber, [FromBody] object data)
        {
            try
            {
                object result = await _groupService.ProcessPostDataAsync(groupId, tableNumber, data);

                if (result is bool && !(bool)result)
                {
                    return BadRequest("Invalid data for the specified group.");
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("group{groupId}/{tableNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProcessGetData(int groupId, int tableNumber)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("group{groupId}/GetAllData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProcessGetAllDataTable(int groupId)
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
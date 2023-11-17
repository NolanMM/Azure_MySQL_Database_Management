using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OTP_Centre.DataModel;
using OTP_Centre.Services;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace OTP_Centre.Controllers
{
    [Route("NolanM/OTPCentre")]
    [ApiController]
    public class OTP_Centre_Controller : ControllerBase
    {
        private readonly OTP_Centre_Services _otpCentreServices;

        public OTP_Centre_Controller(OTP_Centre_Services otpCentreServices)
        {
            _otpCentreServices = otpCentreServices ?? throw new ArgumentNullException(nameof(otpCentreServices));
        }

        [HttpPost("OTPProcess")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessPostDataAsync([FromBody] object data)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(data);

                OTP_Record? otpRecord = JsonSerializer.Deserialize<OTP_Record>(jsonData);

                if (otpRecord != null)
                {
                    if (await _otpCentreServices.OTP_Table_Record_Process(otpRecord))
                    {
                        Console.WriteLine("Time Out");
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }
                else
                {
                    return BadRequest();
                }

                return Ok("Data processed successfully.");
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization error
                return BadRequest($"Error deserializing JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other errors
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
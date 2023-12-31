﻿using Microsoft.AspNetCore.Mvc;
using Server_Side.Database_Services.Output_Schema.Log_Database_Schema;
using System.Text.Json;
using Cloud_Database_Management_System.Repositories.Repository_Group_1.Table_Interface;
using Cloud_Database_Management_System.Security_Services.Security_Table.Data_Models;
using Cloud_Database_Management_System.Services.Security_Services;
using System.Text;
using Cloud_Database_Management_System.Security_Services.OTP_Services;
using Cloud_Database_Management_System.Services.UI_Static_Services;

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
        [HttpGet("Help")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetHelpPage()
        {
            string htmlContent = UI_Static_Services_Control.GenerateHtmlContentHelpPage();
            return Content(htmlContent, "text/html");
        }

        [HttpGet("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetHelpPageLogin()
        {
            string htmlContent = UI_Static_Services_Control.GenerateHtmlContentLoginPage();
            return Content(htmlContent, "text/html");
        }

        [HttpGet("SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetHelpPageRegister()
        {
            string htmlContent = UI_Static_Services_Control.GenerateHtmlContentSignUpPage();
            return Content(htmlContent, "text/html");
        }

        // Make the page when error or okay like register pages
        [Route("CheckAcount/{username}/{password}")]
        [HttpPost]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                bool signInResult = await LoginProcess(username, password);

                if (signInResult)
                {
                    string htmlContent = UI_Static_Services_Control.GenerateLoginSuccessHtml();
                    return Content(htmlContent, "text/html");
                }
                else
                {
                    // Call the LogError method with appropriate parameters
                    await LogError("Login", "Check Account Services", $"Username: {username}, Password: {password}", "Failed", "Sign In failed. Wrong credentials.");
                    string errorMessage = "Invalid username or password.";
                    string htmlContent = UI_Static_Services_Control.GenerateLoginErrorHtml(errorMessage);
                    return Content(htmlContent, "text/html");
                }
            }
            catch (Exception ex)
            {
                // Log the error and return Internal Server Error
                await LogError("Login", "Check Account Services", $"Username: {username}, Password: {password}", "Failed", $"Exception: {ex.Message}");
                string errorMessage = ex.Message;
                string htmlContent = UI_Static_Services_Control.GenerateLoginErrorHtml(errorMessage);
                return Content(htmlContent, "text/html");
            }
        }

        private async Task<bool> LoginProcess(string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrEmpty(password))
            {
                return await Security_Database_Services_Centre.SignInProcess(username, password);
            }

            return false;
        }

        [Route("Register/{registerUsername}/{registerEmail}/{registerPassword}")]
        [HttpPost]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUpRequestProcess(string? registerUsername, string? registerEmail, string? registerPassword)
        {
            try
            {
                // Check if input parameters are valid
                if (!string.IsNullOrWhiteSpace(registerUsername) && !string.IsNullOrEmpty(registerEmail) && !string.IsNullOrEmpty(registerPassword))
                {
                    // Begin the sign-up process
                    OTP_Record? OTP_record_created = await Security_Database_Services_Centre.SignUpProcess_Begin(registerUsername, registerEmail, registerPassword);

                    if (OTP_record_created != null)
                    {
                        string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(OTP_record_created);
                        await LogAndPostDataAsync(jsonBody); // Log and post data
                        string htmlContent = OTP_Module_Services.ResponseRegister(OTP_record_created.OTP_ID);
                        return Content(htmlContent, "text/html");
                    }
                    else
                    {
                        await LogError("SignUpRequestProcess", "N/A", "N/A", "Failed", "Registration failed. Please check your input.");
                        string htmlContent = UI_Static_Services_Control.GenerateGenericRegistrationFailureHtml();
                        return Content(htmlContent, "text/html");
                    }
                }
                else
                {
                    await LogError("SignUpRequestProcess", "N/A", "N/A", "Failed", "Invalid input parameters.");
                    return BadRequest("Invalid input parameters.");
                }
            }
            catch (Exception ex)
            {
                // Log the error and return Internal Server Error
                await LogError("SignUpRequestProcess", "N/A", "N/A", "Failed", $"Exception: {ex.Message}");

                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        private async Task LogAndPostDataAsync(string jsonBody)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string endpointUrl = "https://otpcentrenolanm.azurewebsites.net/NolanM/OTPCentre/OTPProcess";
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    Task.Run(() => client.PostAsync(endpointUrl, content));
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
                await LogError("PostDataAsync", "Sent to OTP Server", jsonBody, "Success", "Data posted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                await LogError("PostDataAsync", "Sent to OTP Server", jsonBody, "Failed", "Data posted failed.");
            }
        }

        // Design a page like register code
        [Route("RegisterVerifyOTP/{OTP_CODE_ID}/{OTP_CODE}")]
        [HttpPost]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterVerifyOTP(string OTP_CODE_ID, string OTP_CODE)
        {
            try
            {
                bool registrationSuccess = await VerifyRegistration(OTP_CODE_ID, OTP_CODE);

                if (registrationSuccess)
                {
                    await LogError("RegisterVerifyOTP", "RegisterVerifyOTP Services", $"OTP_CODE_ID: {OTP_CODE_ID}, OTP_CODE: {OTP_CODE}", "Success", "Registration successful!");
                    return Content(UI_Static_Services_Control.GenerateRegistrationSuccessHtml(), "text/html");
                }

                await LogError("RegisterVerifyOTP", "RegisterVerifyOTP Services", $"OTP_CODE_ID: {OTP_CODE_ID}, OTP_CODE: {OTP_CODE}", "Failed", "Registration failed. Wrong Input or Timeout.");

                Console.WriteLine("Registration failed. Registration failed. Wrong Input Please sign up again after 1 min.");
                return Content(UI_Static_Services_Control.GenerateRegistrationFailureHtml(), "text/html");

            }
            catch (Exception ex)
            {
                await LogError("RegisterVerifyOTP", "RegisterVerifyOTP Services", $"OTP_CODE_ID: {OTP_CODE_ID}, OTP_CODE: {OTP_CODE}", "Failed", $"Exception: {ex.Message}");

                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        private static async Task<bool> VerifyRegistration(string OTP_CODE_ID, string OTP_CODE)
        {
            if (!string.IsNullOrWhiteSpace(OTP_CODE_ID) && !string.IsNullOrEmpty(OTP_CODE))
            {
                return await Security_Database_Services_Centre.SignUpProcessFinish(OTP_CODE_ID, OTP_CODE);
            }

            return false;
        }

        // Post to table design a page to return when ok or error
        [Route("POST/{username}/{password}/group{groupId}/{tableNumber}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessPostDataAsync(string username, string password, int groupId, int tableNumber, [FromBody] object data)
        {
            try
            {
                bool signInResult = await LoginProcess(username, password);

                if (signInResult)
                {
                    string requestType = "POST";
                    string dataString = JsonSerializer.Serialize(data);

                    // Input validation
                    if (groupId == 1 && 0 <= tableNumber && tableNumber <= Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count())
                    {
                        try
                        {
                            object result = await _groupService.ProcessPostDataAsync(groupId, tableNumber, data);
                            Table_Group_1_Dictionary? tableInfo = Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.FirstOrDefault(info => info.Index == tableNumber);

                            if (result is bool && !(bool)result)
                            {
                                return BadRequest($"Error {requestType} To Table: {tableInfo?.TableName}");
                            }

                            return Ok(true);
                        }
                        catch (Exception ex)
                        {
                            string issues = ex.Message;
                            string requestStatus = "Failed";

                            return await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);
                        }
                    }
                    else if (groupId == 1 && (tableNumber < 0 || tableNumber > Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count()))
                    {
                        string issues = "Cannot find the table in the group. Please try again";
                        string requestStatus = "Failed";

                        return await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);
                    }
                    else if (groupId != 1 && 0 <= tableNumber && tableNumber <= Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count())
                    {
                        string issues = "Cannot find the group. Please try again";
                        string requestStatus = "Failed";

                        return await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);
                    }
                    else
                    {
                        string issues = "Someone Try to attack, Server in danger!!!!";
                        string requestStatus = "Failed";
                        requestType = "AttackPost";

                        return await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);
                    }
                }
                else
                {
                    return BadRequest("Sign In failed. Please check your credentials.");
                }
            }
            catch (Exception ex)
            {
                await LogError("ProcessPostDataAsync", "ProcessPostDataAsync Services", $"Username: {username}, Password: {password}", "Failed", $"Exception: {ex.Message}, try to log table: {tableNumber}");

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        // Get data from table create render version route if have time
        [HttpGet("{username}/{password}/group{groupId}/{tableNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessGetData(string username, string password, int groupId, int tableNumber)
        {
            string requestType = "GET_By_Group_ID_Table_Number";

            // Input validation
            if (groupId != 1 || tableNumber < 0 || tableNumber > Table_Group_1_Dictionary.Tablesname_List_with_Data_Type.Count())
            {
                return BadRequest("Invalid group ID or table number");
            }

            try
            {
                // Check login status
                bool signInResult = await LoginProcess(username, password);

                if (signInResult)
                {
                    string dataString = $"GET group ID: {groupId}, table number: {tableNumber}";
                    string requestStatus = "Success";

                    object result = await _groupService.ProcessGetData(groupId, tableNumber);

                    if (result is bool && !(bool)result)
                    {
                        return BadRequest("Invalid data for the specified group.");
                    }
                    else if (result is object || result is string)
                    {
                        return Ok(result);
                    }

                    return NotFound();
                }
                else
                {
                    // Call the LogError method for login failure
                    await LogError(requestType, tableNumber.ToString(), $"GET group ID: {groupId}, table number: {tableNumber}", "Failed", "Access denied. Wrong credentials.");

                    return BadRequest("Access denied. Wrong credentials.");
                }
            }
            catch (Exception ex)
            {
                string dataString = $"GET group ID: {groupId}, table number: {tableNumber}";
                string requestStatus = "Failed";
                string issues = $"An error occurred: {ex.Message}";

                await LogError(requestType, tableNumber.ToString(), dataString, requestStatus, issues);

                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        // Get all data from all table create render version if have time
        [HttpGet("{username}/{password}/group{groupId}/GetAllData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcessGetAllDataTable(string username, string password, int groupId)
        {
            string requestType = "GET_All_Data_Group_1";

            // Input validation
            if (groupId != 1)
            {
                string issues = "Invalid group ID";
                string dataString = $"GET group ID: {groupId} All Tables";
                string requestStatus = "Failed";

                // Call the LogError method for invalid group ID
                return await LogError(requestType, "HACK All Tables", dataString, requestStatus, issues);
            }

            try
            {
                // Check login status
                bool signInResult = await LoginProcess(username, password);

                if (signInResult)
                {
                    string dataString = $"GET group ID: {groupId} All Tables";

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
                else
                {
                    // Call the LogError method for login failure
                    await LogError(requestType, "Get_All_Tables", $"GET group ID: {groupId} All Tables", "Failed", "Access denied. Wrong credentials.");

                    return BadRequest("Access denied. Wrong credentials.");
                }
            }
            catch (Exception ex)
            {
                string dataString = $"GET group ID: {groupId} All Tables";
                string issues = $"An error occurred: {ex.Message}";
                string requestStatus = "Failed";

                // Call the LogError method with appropriate parameters for other errors
                return await LogError(requestType, "Get_All_Tables", dataString, requestStatus, issues);
            }
        }
        private async Task<IActionResult> LogError(string requestType, string tableNumber, string dataString, string requestStatus, string issues)
        {
            bool logStatus = await Analysis_and_reporting_log_data_table.WriteLogData_ProcessAsync(
                requestType,
                DateTime.Now,
                tableNumber,
                dataString,
                requestStatus,
                issues
            );

            if (logStatus)
            {
                Console.WriteLine($"Error: {issues}");
                return NotFound("This server does not exist");
            }
            else
            {
                Console.WriteLine($"Error: {issues}");
                return NotFound("Cannot log the error. It's broke!");
            }
        }
    }
}

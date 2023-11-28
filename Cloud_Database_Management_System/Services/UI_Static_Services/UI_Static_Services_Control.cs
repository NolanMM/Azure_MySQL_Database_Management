using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace Cloud_Database_Management_System.Services.UI_Static_Services
{
    public static class UI_Static_Services_Control
    {
        public static string GenerateHtmlContentHelpPage()
        {
            string htmlContent = @"
                            <!DOCTYPE html>
                            <html lang='en'>
                            <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                <title>API Help Page</title>
                                <style>
                                    body {
                                        font-family: Arial, sans-serif;
                                        margin: 0;
                                        padding: 0;
                                        background: linear-gradient(to bottom right, #cf3b3b, #7e6b02b7);
                                        color: #9e1010; /* Set text color to white for better visibility on gradient background */
                                    }

                                    h1 {
                                        text-align: center;
                                        font-size: 2.5em;
                                        margin-top: 30px;
                                        color: #fff;
                                    }

                                    section {
                                        background-color: rgba(255, 255, 255, 0.9); /* Semi-transparent white background */
                                        border-radius: 10px;
                                        padding: 30px;
                                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.2); /* Box shadow for a subtle lift effect */
                                        margin: 20px auto;
                                        max-width: 800px;
                                        text-align: left;
                                    }

                                    h2 {
                                        color: #CC0000;
                                        font-size: 1.5em;
                                        margin-bottom: 10px;
                                        margin-top: 0;
                                    }

                                    code {
                                        background-color: #f4f4f4;
                                        padding: 5px;
                                        border: 1px solid #ddd;
                                        border-radius: 4px;
                                        color: #CC0000;
                                    }

                                    pre {
                                        background-color: #f9f9f9;
                                        padding: 10px;
                                        border: 1px solid #ddd;
                                        border-radius: 4px;
                                        overflow-x: auto;
                                        margin-top: 20px;
                                        word-wrap: break-word;
                                        color: #CC0000;
                                    }
                                </style>
                            </head>
                            <body>
                                <h1>Welcome to the Data Management System</h1>
                                <section>
                                    <h2>1. Check Account Login</h2>
                                    <p><strong>Endpoint:</strong> <code>/Group1/DatabaseController/CheckAcount/{username}/{password}</code></p>
                                    <pre>curl -X https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/CheckAcount/minhnguyen/Connhenbeo1</pre>
                                </section>
                                <section>
                                    <h2>2. User Registration</h2>
                                    <p><strong>Endpoint:</strong> <code>POST /Group1/DatabaseController/Register/{registerUsername}/{registerEmail}/{registerPassword}</code></p>
                                    <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/Register/minhnguyen1/minhlenguyen02@email.com/Connhenbeo1</pre>
                                </section>
                                <section>
                                    <h2>3. Verify OTP Code</h2>
                                    <p><strong>Endpoint:</strong> <code>POST /Group1/DatabaseController/VerifyOTP/{OTP_CODE_ID}/{otpCode}</code></p>
                                    <p>Replace <code>(OTP_CODE_ID)</code> with the OTP_CODE_ID return after sign up and <code>(otpCode)</code> with the OTP code from email to verify.</p>
                                    <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/VerifyOTP/OTP_CODE_ID/otpCode</pre>
                                </section>
                                <section>
                                    <h2>4. Get Data from Table in Group</h2>
                                    <p><strong>Endpoint:</strong> <code>GET /{username}/{password}/group{groupId}/{tableNumber}</code></p>
                                    <pre>curl -X GET https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/1</pre>
                                </section>
                                <section>
                                    <h2>5. Get All Tables in Group</h2>
                                    <p><strong>Endpoint:</strong> <code>GET /{username}/{password}/group{groupId}/GetAllData</code></p>
                                    <pre>curl -X GET https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/GetAllData</pre>
                                </section>
                                <section>
                                    <h2>6. Post Data to Table in Group</h2>
                                    <p><strong>Endpoint:</strong> <code>POST /{username}/{password}/group{groupId}/{tableNumber}</code></p>
                                    <p>Post data to a specific table in a group.</p>
                                <b> Example for UserView object</b>
                                    <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/0 -H ""Content-Type: application/json"" -d '{""UserView_ID"": 1, ""User_ID"": ""user123"", ""Product_ID"": ""product123"", ""Time_Count"": 2.5, ""Date_Access"": ""2023-11-20T12:00:00""}'</pre>
                            <br><b>
                                    Example for PageView object</b>
                                    <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/1 -H ""Content-Type: application/json"" -d '{""PageView_ID"": 1, ""Page_Name"": ""Page1"", ""Page_Info"": ""Info1"", ""Product_ID"": ""product123"", ""Start_Time"": ""2023-11-20T12:00:00"", ""UserID"": ""user123""}'</pre>
                            <br><b>
                                    Example for Transaction object</b>
                                    <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/2 -H ""Content-Type: application/json"" -d '{""Transaction_ID"": ""trans123"", ""User_ID"": ""user123"", ""Order_Value"": 100.5, ""date"": ""2023-11-20T12:00:00"", ""Details_Products"": ""[{Product_ID: 1 Product_Price: 120.25, Product_Quantity: 1},{Product_ID: 132 Product_Price: 120.25, Product_Quantity: 2},{Product_ID: 231 Product_Price: 221.25, Product_Quantity: 6}]""}'</pre>
                            <br><b>
                                    Example for Feedback object</b>
                                    <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/3 -H ""Content-Type: application/json"" -d '{""Feedback_ID"": 1, ""User_ID"": ""user123"", ""Product_ID"": ""product123"", ""Stars_Rating"": 4.5, ""Date_Updated"": ""2023-11-20T12:00:00""}'</pre>
                                </section>
                            </body>
                            </html>
                            ";
           

            return htmlContent;
        }
        public static string GenerateHtmlContentLoginPage()
        {
            string htmlContent = @"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                     <style>
                        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@500&display=swap');
                        * {
                          margin: 0;
                          padding: 0;
                          font-family: 'Poppins', sans-serif;
                        }
                        section{
                          display: flex;
                          justify-content: center;
                          align-items: center;
                          min-height: 100vh;
                          width: 100%;
                          background-color: black;
                          background-position: center;
                          background-size: cover;
                        }
                        .form-box{
                          position: relative;
                          width: 400px;
                          height: 450px;
                          background: transparent;
                          border: 2px solid rgba(255,255,255,0.5);
                          border-radius: 20px;
                          backdrop-filter: blur(15px);
                          display: flex;
                          justify-content: center;
                          align-items: center;
                        }
                        h2{
                          font-size: 2em;
                          color: #fff;
                          text-align: center;
                        }
                        .inputbox{
                          position: relative;
                          margin: 30px 0;
                          width: 310px;
                          border-bottom: 2px solid #fff;
                        }
                        .inputbox label{
                          position: absolute;
                          top: 50%;
                          left: 5px;
                          transform: translateY(-50%);
                          color: #fff;
                          font-size: 1em;
                          pointer-events: none;
                          transition: .5s;
                        }
                        input:focus ~ label,
                        input:valid ~ label{
                          top: -5px;
                        }
                        .inputbox input {
                          width: 100%;
                          height: 50px;
                          background: transparent;
                          border: none;
                          outline: none;
                          font-size: 1em;
                          padding:0 35px 0 5px;
                          color: #fff;
                        }
                        .inputbox ion-icon{
                          position: absolute;
                          right: 8px;
                          color: #fff;
                          font-size: 1.2em;
                          top: 20px;
                        }
                        .forget{
                          margin: -15px 0 15px ;
                          font-size: .9em;
                          color: #fff;
                          display: flex;
                          justify-content: space-between;  
                        }
                        .forget label input{
                          margin-right: 3px;
                        }
                        .forget label a{
                          color: #fff;
                          text-decoration: none;
                        }
                        .forget label a:hover{
                          text-decoration: underline;
                        }
                        button{
                          width: 100%;
                          height: 40px;
                          border-radius: 40px;
                          background: #fff;
                          border: none;
                          outline: none;
                          cursor: pointer;
                          font-size: 1em;
                          font-weight: 600;
                        }
                        .register{
                          font-size: .9em;
                          color: #fff;
                          text-align: center;
                          margin: 25px 0 10px;
                        }
                        .register p a{
                          text-decoration: none;
                          color: #fff;
                          font-weight: 600;
                        }
                        .register p a:hover{
                          text-decoration: underline;
                        }
                      </style>
                      <title>Login Page</title>
                    </head>
                    <body>
                        <section>
                            <div class='form-box'>
                                <div class='form-value'>
                                    <form id='loginForm'>
                                        <h2>Login</h2>
                                        <div class='inputbox'>
                                            <ion-icon name='person-outline'></ion-icon>
                                            <input id='usernameInput' type='text' required>
                                            <label for=''>Username</label>
                                        </div>
                                        <div class='inputbox'>
                                            <ion-icon name='lock-closed-outline'></ion-icon>
                                            <input id='passwordInput' type='password' required>
                                            <label for=''>Password</label>
                                        </div>
                                        <div class='forget'>
                                            <label for=''><input type='checkbox'>Remember Me  <a href='#'>Forget Password</a></label>
                                        </div>
                                        <button type='button' onclick='performLogin()'>Log in</button>
                                        <div class='register'>
                                            <p>Don't have an account <a href='https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/SignUp'>Register</a></p>
                                        </div>
                                    </form>
                                </div>
                            </div>+
                        </section>
                        <script type='module' src='https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.esm.js'></script>
                        <script nomodule src='https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.js'></script>
                        <script>
                            function performLogin() {
                                var username = document.getElementById('usernameInput').value;
                                var password = document.getElementById('passwordInput').value;
                                var loginUrl = ""https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/CheckAcount/"" + encodeURIComponent(username) + ""/"" + encodeURIComponent(password);
                                console.log('Login URL: ' + loginUrl);
                                window.location.href = loginUrl;
                            }
                        </script>
                    </body>
                    </html>
                    ";
            return htmlContent;
        }
        public static string GenerateHtmlContentSignUpPage()
        {
            string htmlContent = @"
                    <!DOCTYPE html>
                    <html lang='en'>

                    <head>
                        <style>
                            @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@500&display=swap');

                            * {
                                margin: 0;
                                padding: 0;
                                font-family: 'Poppins', sans-serif;
                            }

                            section {
                                display: flex;
                                justify-content: center;
                                align-items: center;
                                min-height: 100vh;
                                width: 100%;
                                background-color: black;
                                background-position: center;
                                background-size: cover;
                            }

                            .form-box {
                                position: relative;
                                width: 400px;
                                height: 450px;
                                background: transparent;
                                border: 2px solid rgba(255, 255, 255, 0.5);
                                border-radius: 20px;
                                backdrop-filter: blur(15px);
                                display: flex;
                                justify-content: center;
                                align-items: center;
                            }

                            .loading-box {
                                display: none;
                                position: absolute;
                                top: 0;
                                left: 0;
                                width: 400px;
                                height: 450px;
                                background-color: black;
                                color: white;
                                flex-direction: column;
                                justify-content: center;
                                align-items: center;
                                border: none;
                                border-radius: 20px;
                                backdrop-filter: blur(15px);
                            }

                            .loading-spinner {
                                border: 4px solid rgba(255, 255, 255, 0.3);
                                border-top: 4px solid #fff;
                                border-radius: 50%;
                                width: 30px;
                                height: 30px;
                                animation: spin 1s linear infinite;
                            }

                            @keyframes spin {
                                0% {
                                    transform: rotate(0deg);
                                }

                                100% {
                                    transform: rotate(360deg);
                                }
                            }

                            h2 {
                                font-size: 2em;
                                color: #fff;
                                text-align: center;
                            }

                            .inputbox {
                                position: relative;
                                margin: 30px 0;
                                width: 310px;
                                border-bottom: 2px solid #fff;
                            }

                            .inputbox label {
                                position: absolute;
                                top: 50%;
                                left: 5px;
                                transform: translateY(-50%);
                                color: #fff;
                                font-size: 1em;
                                pointer-events: none;
                                transition: .5s;
                            }

                            input:focus~label,
                            input:valid~label {
                                top: -5px;
                            }

                            .inputbox input {
                                width: 100%;
                                height: 50px;
                                background: transparent;
                                border: none;
                                outline: none;
                                font-size: 1em;
                                padding: 0 35px 0 5px;
                                color: #fff;
                            }

                            .inputbox ion-icon {
                                position: absolute;
                                right: 8px;
                                color: #fff;
                                font-size: 1.2em;
                                top: 20px;
                            }

                            .forget {
                                margin: -15px 0 15px;
                                font-size: .9em;
                                color: #fff;
                                display: flex;
                                justify-content: space-between;
                            }

                            .forget label input {
                                margin-right: 3px;
                            }

                            .forget label a {
                                color: #fff;
                                text-decoration: none;
                            }

                            .forget label a:hover {
                                text-decoration: underline;
                            }

                            button {
                                width: 100%;
                                height: 40px;
                                border-radius: 40px;
                                background: #fff;
                                border: none;
                                outline: none;
                                cursor: pointer;
                                font-size: 1em;
                                font-weight: 600;
                            }

                            .register {
                                font-size: .9em;
                                color: #fff;
                                text-align: center;
                                margin: 25px 0 10px;
                            }

                            .register p a {
                                text-decoration: none;
                                color: #fff;
                                font-weight: 600;
                            }

                            .register p a:hover {
                                text-decoration: underline;
                            }
                        </style>
                        <title>SignUp</title>
                    </head>

                    <body>
                        <section>
                            <div class='form-box'>
                                <div class='loading-box' id=""loadingBox"">
                                    <h2>Loading...</h2>
                                    <div class=""loading-spinner""></div>
                                </div>
                                <div class='form-value' id=""formValue"">
                                    <form onsubmit='performRegistration(event)'>
                                        <h2>Register</h2>
                                        <div class='inputbox'>
                                            <ion-icon name='person-outline'></ion-icon>
                                            <input type='text' id='usernameInput' required>
                                            <label for='usernameInput'>Username</label>
                                        </div>
                                        <div class='inputbox'>
                                            <ion-icon name='mail-outline'></ion-icon>
                                            <input type='email' id='emailInput' required>
                                            <label for='emailInput'>Email</label>
                                        </div>
                                        <div class='inputbox'>
                                            <ion-icon name='lock-closed-outline'></ion-icon>
                                            <input type='password' id='passwordInput' required>
                                            <label for='passwordInput'>Password</label>
                                        </div>
                                        <button type='submit'>Sign up</button>
                                        <div class='register'>
                                            <p>Already have an account? <a href='https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/Login'>Login</a></p>
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </section>

                        <script>
                            async function performRegistration(event) {
                                event.preventDefault();

                                var username = document.getElementById('usernameInput').value;
                                var email = document.getElementById('emailInput').value;
                                var password = document.getElementById('passwordInput').value;

                                document.getElementById('formValue').style.display = 'none';
                                document.getElementById('loadingBox').style.display = 'flex';

                                var registrationUrl = `https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/Register/${username}/${email}/${password}`;

                                try {
                                    const response = await fetch(registrationUrl, {
                                        method: 'POST',
                                        headers: {
                                            'Content-Type': 'application/json'
                                        },
                                    });

                                    if (!response.ok) {
                                        throw new Error('Registration failed');
                                    }

                                    await new Promise(resolve => setTimeout(resolve, 4000));
                                    window.location.href = registrationUrl;
                                } catch (error) {
                                    document.getElementById('formValue').style.display = 'flex';
                                    document.getElementById('loadingBox').style.display = 'none';

                                    console.error('Registration failed:', error);
                                }
                            }
                        </script>
                    </body>
                    </html>
                    ";
            return htmlContent;
        }
        public static string GenerateLoginSuccessHtml()
        {
            return @"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Login Result</title>
                        <link rel=""stylesheet"" href=""https://fonts.googleapis.com/css2?family=Poppins:wght@500&display=swap"">
                        <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.1/css/all.min.css"">
                        <style>
                            body {
                                font-family: 'Poppins', Tahoma, Geneva, Verdana, sans-serif;
                                text-align: center;
                                margin: 50px;
                                background-color: #000;
                                color: #fff;
                            }

                            div {
                                background-color: transparent;
                                padding: 30px;
                                border-radius: 10px;
                                box-shadow: 0 4px 8px rgba(255, 255, 255, 0.5);
                                max-width: 600px;
                                margin: 0 auto;
                            }

                            h2 {
                                text-align: center;
                                color: #4CAF50;
                            }

                            .tick-icon {
                                font-size: 2em;
                                margin-bottom: 20px;
                                color: #4CAF50;
                                border: 2px solid #4CAF50;
                                border-radius: 50%;
                                width: 50px;
                                height: 50px;
                                display: flex;
                                align-items: center;
                                justify-content: center;
                                box-sizing: border-box;
                            }

                            button {
                                width: 100%;
                                height: 40px;
                                border-radius: 40px;
                                background: #4CAF50;
                                border: none;
                                outline: none;
                                cursor: pointer;
                                font-size: 1em;
                                font-weight: 600;
                                color: #fff;
                                margin-top: 20px;
                            }
                        </style>
                    </head>
                    <body>
                        <div>
                            <div class=""tick-icon""><i class=""fas fa-check""></i></div>
                            <h2>Login Successful</h2>
                            <p>Thank you for logging in. Your account worked properly!</p>
                            <button onclick=""location.href='https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/Login'"">Sign Out</button>
                        </div>
                    </body>
                    </html>
         ";
        }
        public static string GenerateLoginErrorHtml(string? errorMessage)
        {
            if (errorMessage == null) { errorMessage = string.Empty; }
            return "<!DOCTYPE html>" +
                "<html lang='en'>" +
                "<head>" +
                "<meta charset='UTF-8'>" +
                "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                "<title>Login Failed</title>" +
                "<style>" +
                "body {" +
                "    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;" +
                "    text-align: center;" +
                "    margin: 50px;" +
                "    background-color: #f8f8f8;" +
                "}" +
                "div {" +
                "    background-color: #ffffff;" +
                "    padding: 30px;" +
                "    border-radius: 10px;" +
                "    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);" +
                "    max-width: 600px;" +
                "    margin: 0 auto;" +
                "}" +
                "h2 {" +
                "    text-align: center;" +
                "    color: #CC0000; /* Red color for error */" +
                "}" +
                "p {" +
                "    font-size: 20px;" +
                "}" +
                ".error-message {" +
                "    color: #CC0000;" +
                "    margin-top: 20px;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body>" +
                "<div>" +
                "<h2>Login Failed</h2>" +
                "<p>There was an issue with your login attempt. Please check your credentials and try again.</p>" +
                "<div class='error-message'>" +
                "<p>Error Details:</p>" +
                "<p>" + errorMessage + "</p>" +
                "</div>" +
                "</div>" +
                "</body>" +
                "</html>";
        }
        public static string GenerateRegistrationSuccessHtml()
        {
            return "<!DOCTYPE html>" +
                "<html lang='en'>" +
                "<head>" +
                "<meta charset='UTF-8'>" +
                "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                "<title>Registration Successful</title>" +
                "<style>" +
                "body {" +
                "    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;" +
                "    text-align: center;" +
                "    margin: 50px;" +
                "    background-color: #f8f8f8;" +
                "}" +
                "div {" +
                "    background-color: #ffffff;" +
                "    padding: 30px;" +
                "    border-radius: 10px;" +
                "    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);" +
                "    max-width: 600px;" +
                "    margin: 0 auto;" +
                "}" +
                "h2 {" +
                "    text-align: center;" +
                "    color: #00CC00; /* Green color for success */" +
                "}" +
                "p {" +
                "    font-size: 20px;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body>" +
                "<div>" +
                "<h2>Registration Successful</h2>" +
                "<p>Thank you for registering with us!</p>" +
                "<p>Your account has been created successfully.</p>" +
                "</div>" +
                "</body>" +
                "</html>";
        }

        public static string GenerateRegistrationFailureHtml()
        {
            return "<!DOCTYPE html>" +
                "<html lang='en'>" +
                "<head>" +
                "<meta charset='UTF-8'>" +
                "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                "<title>Registration Failed</title>" +
                "<style>" +
                "body {" +
                "    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;" +
                "    text-align: center;" +
                "    margin: 50px;" +
                "    background-color: #f8f8f8;" +
                "}" +
                "div {" +
                "    background-color: #ffffff;" +
                "    padding: 30px;" +
                "    border-radius: 10px;" +
                "    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);" +
                "    max-width: 600px;" +
                "    margin: 0 auto;" +
                "}" +
                "h2 {" +
                "    text-align: center;" +
                "    color: #CC0000; /* Red color for error */" +
                "}" +
                "p {" +
                "    font-size: 20px;" +
                "}" +
                ".error-message {" +
                "    color: #CC0000;" +
                "    margin-top: 20px;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body>" +
                "<div>" +
                "<h2>Registration Failed</h2>" +
                "<p>Registration failed. Wrong input or timeout.</p>" +
                "<p>Please sign up again after 1 minute.</p>" +
                "<div class='error-message'>" +
                "<p>Error Details:</p>" +
                "<p>Registration failed. Wrong input or timeout.</p>" +
                "</div>" +
                "</div>" +
                "</body>" +
                "</html>";
        }

        public static string GenerateGenericRegistrationFailureHtml()
        {
            return "<!DOCTYPE html>" +
                "<html lang='en'>" +
                "<head>" +
                "<meta charset='UTF-8'>" +
                "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                "<title>Registration Failed</title>" +
                "<style>" +
                "body {" +
                "    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;" +
                "    text-align: center;" +
                "    margin: 50px;" +
                "    background-color: #f8f8f8;" +
                "}" +
                "div {" +
                "    background-color: #ffffff;" +
                "    padding: 30px;" +
                "    border-radius: 10px;" +
                "    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);" +
                "    max-width: 600px;" +
                "    margin: 0 auto;" +
                "}" +
                "h2 {" +
                "    text-align: center;" +
                "    color: #CC0000; /* Red color for error */" +
                "}" +
                "p {" +
                "    font-size: 20px;" +
                "}" +
                ".error-message {" +
                "    color: #CC0000;" +
                "    margin-top: 20px;" +
                "}" +
                "</style>" +
                "</head>" +
                "<body>" +
                "<div>" +
                "<h2>Registration Failed</h2>" +
                "<p>Registration failed. Please check your input.</p>" +
                "<div class='error-message'>" +
                "<p>Error Details:</p>" +
                "<p>Registration failed. Please check your input.</p>" +
                "</div>" +
                "</div>" +
                "</body>" +
                "</html>";
        }


    }
}

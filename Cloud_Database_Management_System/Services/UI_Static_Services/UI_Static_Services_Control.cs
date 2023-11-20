using System.Text;

namespace Cloud_Database_Management_System.Services.UI_Static_Services
{
    public static class UI_Static_Services_Control
    {
        public static string GenerateHtmlContentHelpPage()
        {
            string htmlContent =
    @"
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
        <pre>curl -X https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/CheckAcount/johndoe/secret</pre>
    </section>
    <section>
        <h2>2. User Registration</h2>
        <p><strong>Endpoint:</strong> <code>POST /Group1/DatabaseController/Register/{registerUsername}/{registerEmail}/{registerPassword}</code></p>
        <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/Register/johndoe/johndoe@email.com/secret</pre>
    </section>
    <section>
        <h2>3. Verify OTP Code</h2>
        <p><strong>Endpoint:</strong> <code>POST /Group1/DatabaseController/VerifyOTP/{OTP_CODE_ID}/{otpCode}</code></p>
        <p>Replace <code>(username)</code> with the user's username and <code>(otpCode)</code> with the OTP code from email to verify.</p>
        <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/VerifyOTP/OTP_CODE_ID/otpCode</pre>
    </section>
    <section>
        <h2>4. Get Data from Table in Group</h2>
        <p><strong>Endpoint:</strong> <code>GET /{username}/{password}/group{groupId}/{tableNumber}</code></p>
        <pre>curl -X GET https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/johndoe/secret/group1/1</pre>
    </section>
    <section>
        <h2>5. Get All Tables in Group</h2>
        <p><strong>Endpoint:</strong> <code>GET /{username}/{password}/group{groupId}/GetAllData</code></p>
        <pre>curl -X GET https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/johndoe/secret/group1/GetAllData</pre>
    </section>
    <section>
        <h2>6. Post Data to Table in Group</h2>
        <p><strong>Endpoint:</strong> <code>POST /{username}/{password}/group{groupId}/{tableNumber}</code></p>
        <p>Post data to a specific table in a group.</p>
    <b> Example for UserView object</b>
        <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/johndoe/secret/group1/1 -H ""Content-Type: application/json"" -d '{""UserView_ID"": 1, ""User_ID"": ""user123"", ""Product_ID"": ""product123"", ""Time_Count"": 2.5, ""Date_Access"": ""2023-11-20T12:00:00""}'</pre>
<br><b>
        Example for Transaction object</b>
        <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/johndoe/secret/group1/2 -H ""Content-Type: application/json"" -d '{""Transaction_ID"": ""trans123"", ""User_ID"": ""user123"", ""Order_Value"": 100.5, ""date"": ""2023-11-20T12:00:00""}'</pre>
<br><b>
        Example for PageView object</b>
        <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/johndoe/secret/group1/3 -H ""Content-Type: application/json"" -d '{""PageView_ID"": 1, ""Page_Name"": ""Page1"", ""Page_Info"": ""Info1"", ""Product_ID"": ""product123"", ""Start_Time"": ""2023-11-20T12:00:00"", ""UserID"": ""user123""}'</pre>
<br><b>
        Example for Feedback object</b>
        <pre>curl -X POST https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/johndoe/secret/group1/4 -H ""Content-Type: application/json"" -d '{""Feedback_ID"": 1, ""User_ID"": ""user123"", ""Product_ID"": ""product123"", ""Stars_Rating"": 4.5, ""Date_Updated"": ""2023-11-20T12:00:00""}'</pre>
    </section>
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
            <style>
                body {
                    font-family: ""Segoe UI"", Tahoma, Geneva, Verdana, sans-serif;
                    text-align: center;
                    margin: 50px;
                    background-color: #f8f8f8;
                }
                div {
                    background-color: #ffffff;
                    padding: 30px;
                    border-radius: 10px;
                    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                    max-width: 600px;
                    margin: 0 auto;
                }
                h2 {
                    text-align: center;
                    color: #4CAF50; /* Green color for success */
                }
            </style>
        </head>
        <body>
            <div>
                <h2>Login Successful</h2>
                <p>Thank you for logging in. Your account worked properly!</p>
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
    }
}

using Cloud_Database_Management_System.Services.Security_Services.Hashing_Services;

namespace Cloud_Database_Management_System.Services.Security_Services
{
    public static class Security_Database_Services_Centre
    {
        private static string? connection_string { get; set; }

        private static string? Security_Schema {  get; set; }

        public async static bool SignInProcess(string username,string email, string password)
        {
            bool isValid = false;
            return isValid;
        }
        public async static bool SignUpProcess(string username,string email, string password)
        {
            bool isValid = false;

            // Check for the input first no special character

            // Use username and email to create the key to encrypted password
            string raw_key = username + email;
            
            // hasing rawkey and take first 16 character for the input for AES 128bit
            string hasing_value = Hasing_Services.HashString(raw_key);
            string key = hasing_value.Substring(0,16);

            // Check if hasing_value == any UserID value



            return isValid;
        }
        private static bool CheckingSignIn()
        {

        }
    }
}

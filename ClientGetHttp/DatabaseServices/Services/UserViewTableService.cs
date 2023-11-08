using ClientGetHttp.DatabaseServices.Services.Interface_Service;
using Newtonsoft.Json;

namespace ClientGetHttp.DatabaseServices.Services
{
    public class UserViewTableService : IDatabaseServices
    {
        string apiUrl = "https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/group1/0";

        public async Task<List<object>> GetDataServiceAsync()
        {
            List<object>? UserViewData = new List<object>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        UserViewData = JsonConvert.DeserializeObject<List<object>>(jsonContent);
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            return UserViewData;
        }
    }
}

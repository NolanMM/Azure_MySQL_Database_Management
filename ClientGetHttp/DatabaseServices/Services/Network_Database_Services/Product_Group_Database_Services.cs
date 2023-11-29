using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientGetHttp.DatabaseServices.Services.Network_Database_Services
{
    public class ResponseData
    {
        [JsonPropertyName("pid")]
        public string pid { get; set; }

        [JsonPropertyName("sid")]
        public string sid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("sales")]
        public int Sales { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("clicks")]
        public int Clicks { get; set; }

        public override string ToString()
        {
            return $"ProductId: {pid}, SupplierId: {sid}, Name: {Name}, " +
                   $"Description: {Description}, Image: {Image}, Category: {Category}, " +
                   $"Price: {Price}, Stock: {Stock}, Sales: {Sales}, Rating: {Rating}, Clicks: {Clicks}";
        }
    }
    public abstract class Product_Group_Database_Services
    {
        private static readonly string apiUrl = "http://172.105.25.146:8080/api/product?category=&search=";

        public static async Task<List<ResponseData>?> GetDataServiceAsync()
        {
            List<ResponseData>? responseDatas = new List<ResponseData>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        List<ResponseData>? ResponseList = JsonConvert.DeserializeObject<List<ResponseData>?>(jsonContent);

                        if (ResponseList != null)
                        {
                            foreach (ResponseData reponse in ResponseList)
                            {
                                if (ValidateDataAnnotations(reponse))
                                {
                                    responseDatas.Add(reponse);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"HTTP Error: Cannot Deserialize reponse Data Object");
                        }
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
            return responseDatas;
        }

        public static bool ValidateDataAnnotations<T>(T data)
        {
            ValidationContext context = new ValidationContext(data, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(data, context, results, validateAllProperties: true);

            if (!isValid)
            {
                foreach (ValidationResult validationResult in results)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
            }

            return isValid;
        }
    }
}

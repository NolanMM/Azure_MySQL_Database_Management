using ClientGetHttp.DatabaseServices.Services.Network_Database_Services;

namespace ClientGetHttp.DatabaseServices
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Fetch data for a specific table");
            Console.WriteLine("2. Test ProcessDataForGetTableCorrespondingUserID function");
            Console.WriteLine("3. Test Product_Group_Database_Services.GetDataServiceAsync function");
            Console.WriteLine("4. Test Product_Group_Database_Services.GetDataServiceAsync function");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                if (option == 1)
                {
                    Console.WriteLine("Enter the table number (0=UserView, 1=PageView, 2=SaleTransaction, 3=Feedback): ");

                    if (int.TryParse(Console.ReadLine(), out int tableNumber))
                    {
                        var data = await Database_Centre.GetDataForDatabaseServiceID(tableNumber);

                        if (data != null)
                        {
                            Console.WriteLine($"Data received and processed successfully for table {tableNumber}:");

                            foreach (var item in data)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid table number");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                }
                else if (option == 2)
                {
                    Console.WriteLine("Enter the UserID for testing ProcessDataForGetTableCorrespondingUserID function: ");
                    string userID = Console.ReadLine();
                    Dictionary<(string, string), (string, string, string, string)>? result = await Database_Centre.ProcessDataForGetTableCorrespondingUserID_Database(userID);

                    if (result != null)
                    {
                        Console.WriteLine("Results of ProcessDataForGetTableCorrespondingUserID function:");

                        int productIdMaxLength = result.Keys.Max(k => k.Item1.Length);
                        int dateMaxLength = result.Keys.Max(k => k.Item2.Length);
                        int userViewNumberMaxLength = result.Values.Max(v => v.Item1.Length);
                        int pageViewNumberMaxLength = result.Values.Max(v => v.Item2.Length);
                        int totalQuantityMaxLength = result.Values.Max(v => v.Item3.Length);

                        string header = $"| {"ProductId".PadRight(productIdMaxLength)} | {"Date".PadRight(dateMaxLength)} | {"UserViewNumber".PadRight(userViewNumberMaxLength)} | {"PageViewNumber".PadRight(pageViewNumberMaxLength)} | {"TotalQuantity".PadRight(totalQuantityMaxLength)} |";
                        Console.WriteLine(header);

                        foreach (var entry in result)
                        {
                            string productIdColumn = entry.Key.Item1.PadRight(productIdMaxLength);
                            string dateColumn = entry.Key.Item2.PadRight(dateMaxLength);
                            string userViewNumberColumn = entry.Value.Item1.PadRight(userViewNumberMaxLength + 13);
                            string pageViewNumberColumn = entry.Value.Item2.PadRight(pageViewNumberMaxLength + 13);
                            string totalQuantityColumn = entry.Value.Item3.PadRight(totalQuantityMaxLength + 12);

                            string formattedRow = $"| {productIdColumn} | {dateColumn} | {userViewNumberColumn} | {pageViewNumberColumn} | {totalQuantityColumn} | {dateColumn} |";
                            Console.WriteLine(formattedRow);
                        }
                    }

                }
                else if (option == 3)
                {
                    List<ResponseData>? return_response = await Product_Group_Database_Services.GetDataServiceAsync();

                    if (return_response != null)
                    {
                        foreach (var responseData in return_response)
                        {
                            Console.WriteLine($"ProductId: {responseData.pid}, SupplierId: {responseData.sid}, " +
                                              $"Name: {responseData.Name}, Description: {responseData.Description}, " +
                                              $"Image: {responseData.Image}, Category: {responseData.Category}, " +
                                              $"Price: {responseData.Price}, Stock: {responseData.Stock}, " +
                                              $"Sales: {responseData.Sales}, Rating: {responseData.Rating}, Clicks: {responseData.Clicks}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data returned from the service.");
                    }
                }
                else if (option == 4)
                {
                    Console.WriteLine("Enter the UserID for testing ProcessDataForGetTableCorrespondingUserID function: ");
                    string? userID = Console.ReadLine();

                    List<ProductItemData>? result = await Database_Centre.ProcessDataForGetTableCorrespondingUserID(userID);

                    if (result != null && result.Count != 0)
                    {
                        Console.WriteLine("Results of ProcessDataForGetTableCorrespondingUserID function:");
                        int userSellerMaxLength = result.Max(r => r.UserSeller.Length);
                        int productNameMaxLength = result.Max(r => r.ProductName.Length);
                        int todaySaleMaxLength = result.Max(r => r.TodaySale.Length);
                        int todayViewsMaxLength = result.Max(r => r.TodayViews.Length);
                        int productPricesMaxLength = result.Max(r => r.ProductPrices.Length);
                        int dateMaxLength = result.Max(r => r.Date.Length);

                        string header = $"| {"UserSeller".PadRight(userSellerMaxLength)} | {"ProductName".PadRight(productNameMaxLength)} | {"TodaySale".PadRight(todaySaleMaxLength)} | {"TodayViews".PadRight(todayViewsMaxLength)} | {"ProductPrices".PadRight(productPricesMaxLength)} | {"Date".PadRight(dateMaxLength)} |";
                        Console.WriteLine(header);

                        foreach (var entry in result)
                        {
                            string userSellerColumn = entry.UserSeller.PadRight(userSellerMaxLength + 3);
                            string productNameColumn = entry.ProductName.PadRight(productNameMaxLength + 3);
                            string todaySaleColumn = entry.TodaySale.PadRight(todaySaleMaxLength + 2);
                            string todayViewsColumn = entry.TodayViews.PadRight(todayViewsMaxLength + 2);
                            string productPricesColumn = entry.ProductPrices.PadRight(productPricesMaxLength + 2);
                            string dateColumn = entry.Date.PadRight(dateMaxLength);

                            string formattedRow = $"| {userSellerColumn} | {productNameColumn} | {todaySaleColumn} | {todayViewsColumn} | {productPricesColumn} | {dateColumn} |";
                            Console.WriteLine(formattedRow);
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
}

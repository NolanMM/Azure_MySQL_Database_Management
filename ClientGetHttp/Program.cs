namespace ClientGetHttp.DatabaseServices
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Fetch data for a specific table");
            Console.WriteLine("2. Test ProcessDataForGetTableCorrespondingUserID function");

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

                    Dictionary<string, (string, string, string, string)>? result = await Database_Centre.ProcessDataForGetTableCorrespondingUserID(userID);

                    if (result != null)
                    {
                        Console.WriteLine("Results of ProcessDataForGetTableCorrespondingUserID function:");

                        foreach (var entry in result)
                        {
                            Console.WriteLine($"ProductID: {entry.Key}, UserViewNumber: {entry.Value.Item1}, PageViewNumber: {entry.Value.Item2}, TotalQuantity: {entry.Value.Item3}, Date: {entry.Value.Item4}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("An error occurred or the result is null.");
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

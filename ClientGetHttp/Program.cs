namespace ClientGetHttp.DatabaseServices
{
    class Program
    {
        static async Task Main(string[] args)
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
    }
}
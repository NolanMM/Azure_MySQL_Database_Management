using Security_Services_Dev_Env.Services.Security_Services;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Welcome to the Security System");
            Console.WriteLine("1. Sign In");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice (1, 2, or 3): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter your username: ");
                    string? signInUsername = Console.ReadLine();

                    Console.Write("Enter your password: ");
                    string? signInPassword = Console.ReadLine();

                    bool signInResult = await Security_Database_Services_Centre.SignInProcess(signInUsername, signInPassword);

                    if (signInResult)
                    {
                        Console.WriteLine("Sign In successful!");
                    }
                    else
                    {
                        Console.WriteLine("Sign In failed. Please check your credentials.");
                    }
                    break;

                case "2":
                    Console.Write("Enter your username: ");
                    string? registerUsername = Console.ReadLine();

                    Console.Write("Enter your email: ");
                    string? registerEmail = Console.ReadLine();

                    Console.Write("Enter your password: ");
                    string? registerPassword = Console.ReadLine();


                    bool registerResult = await Security_Database_Services_Centre.SignUpProcess(registerUsername, registerEmail, registerPassword);

                    if (registerResult)
                    {
                        Console.WriteLine("Registration successful!");
                    }
                    else
                    {
                        Console.WriteLine("Registration failed. Please check your input.");
                    }

                    break;

                case "3":
                    Console.WriteLine("Exiting the program. Goodbye!");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

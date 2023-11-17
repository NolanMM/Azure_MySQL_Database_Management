using Security_Services_Dev_Env.Security_Services.Security_Table.Data_Models;
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
            Console.WriteLine("3. Verify OTP Code");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1, 2, 3 or 4): ");
            string? choice = Console.ReadLine();

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

                    if (!string.IsNullOrWhiteSpace(registerUsername) && !string.IsNullOrEmpty(registerEmail) && !string.IsNullOrEmpty(registerPassword))
                    {
                        OTP_Record? OTP_record_created = await Security_Database_Services_Centre.SignUpProcess_Begin(registerUsername, registerEmail, registerPassword);

                        if (OTP_record_created != null)
                        {
                            Console.WriteLine("Register Request Recorded successful!");
                            Console.WriteLine("Your Register ID to Confirm the OTP code is: " + OTP_record_created.OTP_ID);

                            // Start the OTP_Table_Record_Process in a separate thread without waiting for its result
                            //Task.Run(() => Security_Database_Services_Centre.OTP_Table_Record_Process(OTP_record_created));
                            if(await Security_Database_Services_Centre.OTP_Table_Record_Process(OTP_record_created))
                            {
                                Console.WriteLine("Time Out");
                            }else
                            {
                                Console.WriteLine("Error");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Registration failed. Please check your input.");
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
                case "3":
                    Console.Write("Enter your Register Requested ID: ");
                    string? OTP_CODE_ID = Console.ReadLine();

                    Console.Write("Enter your OTP Code: ");
                    string? OTP_CODE = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(OTP_CODE_ID) && !string.IsNullOrEmpty(OTP_CODE))
                    {
                        if (await Security_Database_Services_Centre.SignUpProcessFinish(OTP_CODE_ID, OTP_CODE))
                        {
                            Console.WriteLine("Register Account successful!");
                            break;
                        }
                        Console.WriteLine("Registration failed. Please check your input.");
                        break;
                    }
                    Console.WriteLine("Input Cannot be Null");
                    break;
                case "4":
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

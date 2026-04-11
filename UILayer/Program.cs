using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using LogicLayer; 

namespace UI
{
    // WhoAmI Console App v0.1.0-alpha
    // - Initial project setup

     // WhoAmI Console App v0.2.0-alpha
    // - Feature: Live SQLite database support

    // WhoAmI Console App v0.2.1-alpha
    // - Fix: Prevent empty/whitespace name bypass validation

    // WhoAmI Console App v0.3.0-alpha
    // - Feature: Color-coded console outputs
    // - Fix: None
    class Program
    {
        private static AppService appService = new AppService();
        static string MAINMENU = """
        1. Create a profile
        2. Find a profile
        3. Update a profile
        4. Delete a profile
        5. Exit Program
        """;
        static string ASCII = """ 
            ========================================

                    STUDENT ENROLLMENT SYSTEM       
                        
            ========================================
            """;
        static void Main()
        {
            if (appService.startDb())
            {
                App();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Connection Failed\n");
                Console.ResetColor();
            }
        }
        static void App()
        {
            bool isAppRun = true;

            while (isAppRun)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(ASCII + "\n");
                Console.ResetColor();
                Console.WriteLine(MAINMENU + "\n");

                Console.Write("> ");
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Option cannot be empty.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                switch (userInput)
                {
                    case "1":
                        CreateProfile();
                        break;

                    case "2":
                        FindProfile();
                        break;

                    case "3":
                        UpdateProfile();
                        break;

                    case "4":
                        DeleteProfile();
                        break;

                    case "5":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Closing Program...");
                        Console.ResetColor();
                        isAppRun = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n'{userInput}' is not a valid option");
                        Console.ResetColor();

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void CreateProfile()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("Create a new profile (X to exit)");
                Console.Write("> ");
                string newProfile = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newProfile))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Profile cannot be empty.\n");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                newProfile = newProfile.ToLower();

                if(newProfile == "x")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("- Returning to Main Menu...\n");
                    Console.ResetColor();

                    Console.ReadKey();
                    return;
                }

                if (appService.isSmaller(newProfile))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- \"{newProfile}\" is too short, 3 is the minimum lenght.");
                    Console.ResetColor();

                    Console.ReadKey();
                }
                else if(appService.isLarger(newProfile))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- \"{newProfile}\" has reached the limit, 16 is the maximum lenght.");
                    Console.ResetColor();

                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Profile successfully created!\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("- Returning to Main Menu...");
                    Console.ResetColor();
                    
                    Console.ReadKey();
                    return;
                    
                }
            }   
            
        }
        static void FindProfile()
        {   
            bool isRunning = true;

            while(isRunning)
            {
                Console.Clear();

                Console.WriteLine("Enter your name (X to exit)");
                Console.Write("> ");
                string Name = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(Name)) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Name cannot be empty.\n");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                Name = Name.ToLower();

                if(Name.ToLower() == "x")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("- Returning to Main Menu...\n");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }

                if (appService.isNameValid(Name))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"- Hello, {Name}!\n");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- \"{Name}\" is not an enrolled student.\n");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
        }
        static void UpdateProfile()
        {
            Console.WriteLine("Update a profile");
            Console.ReadKey();
        }
        static void DeleteProfile()
        {
            Console.WriteLine("Delete a profile");
            Console.ReadKey();
        }
        
    }
}


using System.Reflection.Metadata;
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
        static AppService appService = new AppService();
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
            if (appService.dbInit())
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

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ASCII + "\n");
                Console.ResetColor();
                Console.WriteLine(MAINMENU + "\n");

                Console.Write("> ");
                string userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("- Option cannot be empty.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                switch (userInput)
                {
                    case "1":
                        createProfile();
                        break;

                    case "2":
                        findProfile();
                        break;

                    case "3":
                        updateProfile();
                        break;

                    case "4":
                        deleteProfile();
                        break;

                    case "5":
                        Console.ForegroundColor = ConsoleColor.Cyan;
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
        static void createProfile()
        {
            Console.WriteLine("Create a profile");
            Console.ReadKey();
        }
        static void findProfile()
        {   
            bool isRunning = true;

            while(isRunning)
            {
                Console.Clear();
                Console.Write("> Enter your name (X to exit): ");
                string Name = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(Name)) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Name cannot be empty.\n");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                Name.ToLower();

                if(Name.ToLower() == "x")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("- Back to Main Menu...\n");
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
        static void updateProfile()
        {
            Console.WriteLine("Update a profile");
            Console.ReadKey();
        }
        static void deleteProfile()
        {
            Console.WriteLine("Delete a profile");
            Console.ReadKey();
        }
        
    }
}


using System.Reflection.Metadata;
using LogicLayer; 

namespace UI
{

    // WhoAmI Console App version : 0.1.0 - Initial Project Setup 
    // WhoAmI Console App version : 0.2.1 - (Feature) Live database (Sqlite Only) | (Fix) Name validation preveneting emptry whitespaces to bypass the read.
    // WhoAmI Console App version : 0.3.1 - (Feature) Color coded text outputs | (Fix) None
    class Program
    {
        static void Main()
        {
            AppService appService = new AppService();

            const string ASCII = """ 
            ========================================

                    STUDENT ENROLLMENT SYSTEM       
                        
            ========================================
            """;

            if (appService.dbInit())
            {
                Console.WriteLine(ASCII);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Status: Connected\n");
                Console.ResetColor();
                App(appService);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Status: Disconnected\n");
                Console.ResetColor();
            }
  
        }

        static void App(AppService appService)
        {
            bool isRunning = true;

            while(isRunning)
            {
                Console.Write("> Enter your name (X to exit): ");
                string Name = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(Name)) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Name cannot be empty.\n");
                    Console.ResetColor();
                    continue;
                }

                Name.ToLower();

                if(Name.ToLower() == "x")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("- Closing Program...");
                    Console.ResetColor();
                    break;
                }

                if (appService.isNameValid(Name))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"- Hello, {Name}!\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- \"{Name}\" is not an enrolled student.\n");
                    Console.ResetColor();
                }
            }
        }
    }
}


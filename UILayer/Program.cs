using LogicLayer; 

namespace UI
{

    // WhoAmI Console App version : 0.1.0 - Initial Project Setup 
    // WhoamI Console App version : 0.2.1 - (Feature) Live database (Sqlite Only) | (Fix) Name validation preveneting emptry whitespaces to bypass the read. 
    class Program
    {
        static void Main()
        {
            AppService appService = new AppService();

            if(appService.dbInit()) 
            {
                Console.WriteLine("--- Connection secured! ---\n"); 
                App(appService);
            }
            else Console.Write("--- Failed to connect... ---");
  
        }

        static void App(AppService appService)
        {
            bool isRunning = true;

            while(isRunning)
            {
               
                Console.Write("Enter your name (type \"X\" to exit): ");
                string Name = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(Name)) 
                {
                    Console.WriteLine("Name cannot be empty.");
                    continue;
                }

                Name.ToLower();

                if(Name.ToLower() == "x")
                {
                    break;
                }

                if (appService.isNameValid(Name))
                {
                    Console.WriteLine($"Hello, {Name}!");
                }
                else
                {
                    Console.WriteLine($" \"{Name}\" is not an enrolled student.");
                }
            }
        }
    }
}


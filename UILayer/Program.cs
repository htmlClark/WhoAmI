using LogicLayer; 

namespace UI
{

    // WhoAmI Console App version : 0.1.0 
    class Program
    {
        static void Main()
        {
            AppService appService = new AppService();
            appService.dbInit();
            
            bool isRunning = true;

            Console.WriteLine("db Initialized");
            
            while(isRunning)
            {
                
                Console.Write("Enter your name (type \"X\" to exit): ");
                string Name = Console.ReadLine().ToLower();

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

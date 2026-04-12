using System.Data.Common;
using System.Drawing;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Core;
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

    // WhoAmI Console App v0.3.1-alpha
    // Feature: (New) Added a show profile option
    // Fix: Duplicated profile SQLite exception 
    // Fix: Prevent special characters 

    class Program
    {
        private static AppService appService = new AppService();
        static string MAINMENU = """
        1. Create a profile
        2. Find a profile
        3. Update a profile
        4. Delete a profile
        5. Show All Profiles
        6. Exit Program
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

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("- Closing Program...");
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
                        ShowAllProfiles();
                        break;
                    case "6":
                        Console.Write("\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Closing Program...");
                        Console.ResetColor();
                        isAppRun = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"- '{userInput}' is not a valid option\n");
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

                Console.WriteLine("Enter a new profile (X to exit)");
                Console.Write("> ");
                string newProfile = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(newProfile))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Profile cannot be empty.\n");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    continue;
                }

                if(newProfile == "x")
                {
                    Console.Write("\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Returning to Main Menu...");
                    Console.ResetColor();

                    Console.ReadKey(true);
                    return;
                }

                if (appService.isSpecialCharacters(newProfile))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- \"{newProfile}\" contains special character(s), please use letters only.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    continue;
                }

                if (appService.findDuplicate(newProfile))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Attempting to duplicate a profile, please try again.\n");
                    Console.ResetColor();
                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Press any key to continue...");
                    Console.ResetColor();

                    Console.ReadKey(true);
                    continue;
                }

                if (appService.isSmaller(newProfile))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- \"{newProfile}\" is too short, 3 is the minimum lenght.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    continue;
                }
                else if(appService.isLarger(newProfile))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- \"{newProfile}\" has reached the limit, 16 is the maximum lenght.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    continue;
                }
                else
                {
                    appService.addProfile(newProfile);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("- Profile successfully created!\n");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Returning to Main Menu...");
                    Console.ResetColor();
                    
                    Console.ReadKey(true);
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

                Console.WriteLine("Enter a profile to look (X to exit)");
                Console.Write("> ");
                string Name = Console.ReadLine().ToLower();

                if(string.IsNullOrWhiteSpace(Name)) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Name cannot be empty.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    continue;
                }

                if(Name.ToLower() == "x")
                {
                    Console.Write("\n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("- Returning to Main Menu...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    return;
                }

                if (appService.profileToLook(Name))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"- Looking at {Name}'s profile:\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- \"{Name}\" is not an enrolled student.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
            }
        }
        static void UpdateProfile()
        {
           Console.Clear();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Enter a profile name to update (X to exit)");
                Console.Write("> ");
                string profileToUpdate = Console.ReadLine().ToLower();

                if(profileToUpdate == "x") return;

                if (string.IsNullOrWhiteSpace(profileToUpdate))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Field cannot be empty.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
                else
                {
                    while(true)
                    {
                        if(appService.profileToLook(profileToUpdate))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"- Hello, {profileToUpdate}!\n");
                            Console.ResetColor();

                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true);

                            while (true)
                            {
                                Console.Clear();

                                Console.WriteLine($"Enter your new profile name for \"{profileToUpdate}\" (X to exit)");
                                Console.Write("> ");
                                string updatedProfile = Console.ReadLine().ToLower();

                                if(updatedProfile == "x") return;
                                if (appService.isSpecialCharacters(updatedProfile))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"- \"{updatedProfile}\" contains special character(s), please use letters only.\n");
                                    Console.ResetColor();

                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey(true);
                                    continue;
                                }
                                if (appService.findDuplicate(updatedProfile))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"- \"{updatedProfile}\" already used, use a different one.\n");
                                    Console.ResetColor();
                                    
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey(true);
                                    continue;
                                }

                                if (string.IsNullOrWhiteSpace(updatedProfile))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("- Field cannot be empty.\n");
                                    Console.ResetColor();

                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey(true);
                                }
                                else
                                {
                                    appService.updateProfile(profileToUpdate, updatedProfile);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"- Profile successfully updated to \"{updatedProfile}\".\n");
                                    Console.ResetColor();

                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey(true);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"- {profileToUpdate} profile does not exist\n");
                            Console.ResetColor();

                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey(true);
                            break;
                        }
                    }
                }
            }
        }
        static void DeleteProfile()
        {
            Console.Clear();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Type a profile to delete (X to exit)");
                Console.Write("> ");
                string profileToDelete = Console.ReadLine().ToLower();

                if(profileToDelete == "x") return;

                if (string.IsNullOrWhiteSpace(profileToDelete))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("- Field cannot be empty.\n");
                    Console.ResetColor();

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    continue;
                }
                else
                {
                    if (appService.profileToLook(profileToDelete))
                    {
                        appService.DeleteProfileInDb(profileToDelete);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"- {profileToDelete} successfully removed. \n");
                        Console.ResetColor();

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"- {profileToDelete} does not exist.\n");
                        Console.ResetColor();

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                    }
                }
            }
        }
        static void ShowAllProfiles()
        {
            Console.Clear();

            List<Student> students = appService.FetchProfileFromDb();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Current Profiles: \n");
            Console.ResetColor();

            Console.WriteLine("=================================");
            Console.WriteLine($"{"ID",-9} | {"STUDENT NAME",-20}");
            Console.WriteLine("=================================");  

            foreach(var s in students)
            {
                Console.WriteLine($"Id: {s.Id, -5} | Student: {s.Name, -20}");
            }

            Console.WriteLine("=================================\n");
            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey(true);
        }
    }
}


using software_engineering_project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using test.Utilities;

namespace InitialTestApp
{
    class Program
    {
        private static List<string> databasePaths = new List<string>();
        private static string databaseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "softwareEngineeringProject");   // Top db directory
        private static List<Guest> guests = new List<Guest>();
        private static List<Availability> availabilities = new List<Availability>();

        enum DatabasePath
        {
            GUESTS,
            AVAILABILITIES,
        };

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit); // Setup code to run on exit

            Menu.MenuType selectedMenu = Menu.MenuType.MAIN_MENU;   // Default selected menu is the main menu

            // Add database json files
            databasePaths.Add(Path.Combine(databaseDir, "guests.json"));            // Full path to a directory json file
            databasePaths.Add(Path.Combine(databaseDir, "availabilities.json"));    // Full path to a directory json file

            Directory.CreateDirectory(databaseDir);         // Create the directory located in the users Roaming directory

            // For all file paths, check if they exists, if not, create an empty file
            foreach (string f in databasePaths)
            {
                if (!File.Exists(f))
                {
                    using (StreamWriter sw = File.CreateText(f));
                }
            }
            Availability.SetMaxNumberOfRoomsPerDay(3);      // Set the maximum number of rooms

            // Add all menu options and functionality
            // MAIN_MENU
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Save to db", true, doOption: () => {
                Database.WriteToJsonFile<List<Guest>>(databasePaths[(int)DatabasePath.GUESTS], guests);
                Database.WriteToJsonFile<List<Availability>>(databasePaths[(int)DatabasePath.AVAILABILITIES], availabilities);
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Read from db", true, doOption: () => {
                guests = Database.ReadFromJsonFile<List<Guest>>(databasePaths[(int)DatabasePath.GUESTS]);
                availabilities = Database.ReadFromJsonFile<List<Availability>>(databasePaths[(int)DatabasePath.AVAILABILITIES]);
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Print users", false, doOption: () => {
                Console.Clear();
                Console.WriteLine("All guests:\n----------------------------");

                if (guests != null)
                {
                    foreach (Guest guest in guests)
                    {
                        guest.PrintInformation();
                        if (!Equals(guest, guests.Last()))
                        {
                            Console.WriteLine("-------------");
                        }
                        else
                        {
                            Console.WriteLine("\n\n\n");
                        }
                    }
                }
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Make Reservation", true, doOption: () => {
                Console.WriteLine("Choose reservsation type");
                selectedMenu = Menu.MenuType.RESERVATION_MENU;
                Console.Clear();
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Check if day is available", false, doOption: () => {
                if (Availability.CheckAvailability(new DateTime(2022, 3, 31), new DateTime(2022, 4, 1), availabilities))
                {
                    Console.WriteLine("ITS AVAILIBLE!");
                } else
                {
                    Console.WriteLine("ITS NOT AVAILIBLE");
                }
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Add availibility", false, doOption: () => {
                //availibilities.Add(new Availability(new DateTime(2022, 3, 31)));
                Availability.UpdateAvailability(availabilities, new Availability(new DateTime(2022, 3, 31)));

                foreach(Availability a in availabilities)
                {
                    Console.WriteLine(a.numberOfRoomsAvailable);
                }
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Add availibility 2", false, doOption: () => {
                //availibilities.Add(new Availability(new DateTime(2022, 3, 31)));
                Availability.UpdateAvailability(availabilities, new Availability(new DateTime(2022, 4, 1)));

                foreach (Availability a in availabilities)
                {
                    Console.WriteLine(a.numberOfRoomsAvailable);
                }
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Test echo", false, doOption: () => {
                Console.Clear();
                Console.WriteLine("ECHO - Test page 1");
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Change to secondary menu", true, doOption: () => {
                Console.WriteLine("Changed to secondary menu!");
                selectedMenu = Menu.MenuType.SECONDARY_MENU;
                Console.Clear();
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Add guest", false, doOption: () => {
                Guest newGuest = new Guest();
                guests.Add(newGuest);

                // User name will be optional
                UserInput.PromptUser(false, "User name (if none, press enter): ");
                newGuest.name = UserInput.HandleUserInputString();

                // Credit card number is required and must be correct at input
                do
                {
                    UserInput.PromptUser(false, "*Credit card number: ");
                } while (!newGuest.SetCreditCardNumber(UserInput.HandleUserInputLong()));

                Reservation res = new Reservation(DateTime.Now, DateTime.Now.AddDays(5));
                //UserInput.PromptUser(false, "Number of nights: ");
                //res.numberOfNights = UserInput.HandleUserInputInt();

                newGuest.reservations.Add(res);


                Console.WriteLine("Guest added!");

                UserInput.EnterToContinue();

                Console.Clear();
            });
            //Report Printing Section
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Print Reports", false, doOption: () => {       
                Console.WriteLine("Changed to report menu!");
                selectedMenu = Menu.MenuType.REPORT_MENU;
                Console.Clear();
            });
            Menu.AddMenuOption(Menu.MenuType.REPORT_MENU, "Expected Occupancy Report(M)", false, doOption: () => {
                Console.WriteLine("Printing Expected Occupancy Report");
            });
            Menu.AddMenuOption(Menu.MenuType.REPORT_MENU, "Expected Room Income Report(M)", false, doOption: () => {
                Console.WriteLine("Printing Expected Room Income Report");
            });
            Menu.AddMenuOption(Menu.MenuType.REPORT_MENU, "Incentive Report(M)", false, doOption: () => {
                Console.WriteLine("Printing Incentive Report");
            });
            Menu.AddMenuOption(Menu.MenuType.REPORT_MENU, "Daily Arrivals Report", false, doOption: () => {
                Console.WriteLine("Printing Daily Arrivals Report");
            });
            Menu.AddMenuOption(Menu.MenuType.REPORT_MENU, "Daily Occupancy Report", false, doOption: () => {
                Console.WriteLine("Printing Daily Occupancy Report");
            });
            Menu.AddMenuOption(Menu.MenuType.REPORT_MENU, "Accomodation Bill", false, doOption: () => {
                Console.WriteLine("Printing Accomodation Bill");
            });
            Menu.AddMenuOption(Menu.MenuType.REPORT_MENU, "Back to Main Menu", false, doOption: () => {
                Console.WriteLine("Changed to Main menu!");
                selectedMenu = Menu.MenuType.MAIN_MENU;
                Console.Clear();
            });
            Menu.AddMenuOption(Menu.MenuType.MAIN_MENU, "Exit", false, doOption: () => {
                if (UserInput.HandleYNPrompt("Are you sure you want to exit?"))
                {
                    Environment.Exit(0);    // This will call the OnProcessExit function because of the event handler setup earlier
                }
            });

            // RESERVATION_MENU
            Menu.AddMenuOption(Menu.MenuType.RESERVATION_MENU, "Prepaid", false, doOption: () => {
                Console.WriteLine("Prepaid reservation selected");
            });
            Menu.AddMenuOption(Menu.MenuType.RESERVATION_MENU, "Sixty-days-in-advance", false, doOption: () => {
                Console.WriteLine("Sixty-days-in-advance reservation selected");
            });
            Menu.AddMenuOption(Menu.MenuType.RESERVATION_MENU, "Conventional", false, doOption: () => {
                Console.WriteLine("Conventional reservation selected");
            });
            Menu.AddMenuOption(Menu.MenuType.RESERVATION_MENU, "Incentive", false, doOption: () => {
                Console.WriteLine("Incentive reservation selected");
            });
            Menu.AddMenuOption(Menu.MenuType.RESERVATION_MENU, "Back to main menu", false, doOption: () => {
                selectedMenu = Menu.MenuType.MAIN_MENU;
            });

            // SECONDARY_MENU
            Menu.AddMenuOption(Menu.MenuType.SECONDARY_MENU, "Test echo", false, doOption: () => {
                Console.WriteLine("ECHO - Test page 2");
            });
            Menu.AddMenuOption(Menu.MenuType.SECONDARY_MENU, "Change to primary menu", true, doOption: () => {
                Console.WriteLine("Changed to primary menu!");
                selectedMenu = Menu.MenuType.MAIN_MENU;
                Console.Clear();
            });
            Menu.AddMenuOption(Menu.MenuType.SECONDARY_MENU, "Go to third menu", false, doOption: () => {
                selectedMenu = Menu.MenuType.THIRD_MENU;
            });

            // THIRD_MENU
            Menu.AddMenuOption(Menu.MenuType.THIRD_MENU, "Test", false, doOption: () => {
                Console.WriteLine("TESTING");
            });
            Menu.AddMenuOption(Menu.MenuType.THIRD_MENU, "Go to second menu", false, doOption: () => {
                selectedMenu = Menu.MenuType.SECONDARY_MENU;
            });

            // Add automatic events
            AutomaticEvent.AddEvent(DateTime.Now.AddSeconds(5), DateEvent.EventType.NO_REPEATE, action: () =>
            {
                Console.WriteLine("IT'S BEEN 5 SECONDS!!");
            });

            AutomaticEvent.StartCheckingEvents();   // Check all automatic events, call them if date time matches

            // Loop
            while (true)
            {
                // Testing with secondary menu
                Console.WriteLine("------------- Ophelia's Oasis -------------");
                Menu.PrintMenu(selectedMenu);
                UserInput.PromptUser(true);
                Menu.HandleOption(selectedMenu, UserInput.HandleUserInputInt());
            }
        }

        // Gets called whenever the program is being closed
        static void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("Exiting");
            AutomaticEvent.TerminateEventChecking();
        }
    }
}

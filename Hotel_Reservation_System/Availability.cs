using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace software_engineering_project
{
    internal class Availability
    {
        private static int maxNumberOfRoomsPerDay = 5;

        public DateTime day;    // The date that's being kept track of
        public int numberOfRoomsAvailable = maxNumberOfRoomsPerDay - 1; // When instance of object is created, the number of rooms available should be decremented

        public Availability(DateTime day)
        {
            this.day = day;
        }

        // Sets the "global" maxNumberOfRoomsPerDay
        public static void SetMaxNumberOfRoomsPerDay(int maxNum)
        {
            maxNumberOfRoomsPerDay = maxNum;
        }

        // Check the availibility for a single room given a specific start and end date
        //  Searches through a list of availabilities and checks if any of the Availability days (DateTime) intersect
        //  between the start and end date given to the function
        public static bool CheckAvailability(DateTime startDate, DateTime endDate, List<Availability> availabilities)
        {
            TimeSpan dateDifference = endDate - startDate;
            Console.WriteLine(dateDifference.Days);

            for (int i = 0; i < dateDifference.Days; i++)
            {
                Availability availibility = availabilities.Where(d => d.day == startDate).FirstOrDefault();
                if (availibility != null)
                {
                    if (availibility.numberOfRoomsAvailable <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Trys to add an availability object to the list of availabilities if it doesn't already exist
        //      If it does exist, just decrement the number of rooms available
        public static void UpdateAvailability(List<Availability> availabilities, Availability availability)
        {
            Availability avail = availabilities.Where(a => a.day == availability.day).FirstOrDefault();
            if (avail == null)
            {
                availabilities.Add(availability);
                Console.WriteLine("Added");
            } else
            {
                if (avail.numberOfRoomsAvailable > 0)
                {
                    avail.numberOfRoomsAvailable--;
                    Console.WriteLine("Exists, decremented rooms available");
                } else
                {
                    Console.WriteLine("There are no more rooms available");
                }
            }
        }
    }
}

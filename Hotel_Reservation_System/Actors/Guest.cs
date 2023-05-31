using System;
using System.Collections.Generic;
using System.Text;

namespace InitialTestApp
{
    class Guest
    {
        public string name;
        public long creditCardNumber = 0;
        public List<Reservation> reservations;  // List of reservations (in case the same user is allowed multiple reservations

        public Guest()
        {
            reservations = new List<Reservation>();
        }

        // Add a reservation to the reservation list
        public void AddReservation(Reservation reservation)
        {
            this.reservations.Add(reservation);
        }

        // Update the credit card number if it is valid
        public bool SetCreditCardNumber(long number)
        {
            if (Creditcard.IsValid(number))
            {
                creditCardNumber = number;
            } else
            {
                ErrorHandler.HandleError(ErrorHandler.ErrorType.INVALID_CREDIT_CARD_NUMBER);
                return false;
            }

            return true;
        }

        // Print out all of the guest's information
        public void PrintInformation()
        {
            Console.WriteLine("Name: " + (name == "" ? "<NameNotSpecified>" : name));
            Console.WriteLine("Credit card number: " + (creditCardNumber == 0 ? "<NumberNotSpecified>" : Convert.ToString(creditCardNumber)));

            Console.WriteLine("Reservations:");
            foreach (Reservation res in reservations) {
                Console.WriteLine("Room number: " + res.roomNumber);
                Console.WriteLine("Number of Nights: " + res.numberOfNights);

                Console.Write("\n");    // Just adds a space
            }
        }
    }
}

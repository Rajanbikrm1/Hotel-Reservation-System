using System;
using System.Collections.Generic;
using System.Text;

namespace InitialTestApp
{
    class Reservation
    {
        enum ReservationType
        {
            PREPAID,
        }

        ReservationType reservationType;
        DateTime startDate, endDate, datePaidInAdvance;
        public int numberOfNights, roomNumber;
        float totalCharge, amountPaidInAdvance, currentBaseRate;
        bool isCanceled = false, isCheckedIn = false, isCheckedOut = false;

        public Reservation(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;


        }
    }
}

using System;
using System.Collections.Generic;

namespace Services.Contracts.Request
{
    public class GetCalendarRequest
    {
        public int RentalId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public int NumberOfNights { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts.Request
{
    public class AddBookingRequest
    {
        public int RentalId { get; set; }
        public int NumberOfNigths { get; set; }
        public DateTime StartDate { get; set; }
    }
}

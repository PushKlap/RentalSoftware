using System;

namespace Services.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public int Nights { get; set; }
        public int RentalId { get; set; }
        public DateTime Start { get; set; }

    }
}
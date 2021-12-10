using DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Models
{

    public class CalendarViewModel
    {
        public int RentalId { get; set; }
        public List<CalendarDateViewModel> Dates { get; set; }
    }
}

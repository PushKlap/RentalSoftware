using Services.Models;
using System;
using System.Collections.Generic;

namespace VacationRental.Models
{
    public class CalendarDateViewModel
    {
        public CalendarDateViewModel(DateTime date)
        {
            this.Date = date;
            this.Bookings = new List<CalendarBookingViewModel>();
            this.PreparationTimes = new List<UnitViewModel>();
        }
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
        public List<UnitViewModel> PreparationTimes { get; set; }
    }
}

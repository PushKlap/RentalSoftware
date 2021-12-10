using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Request;
using Services.Interfaces;
using Services.Models;
using System;
using System.Threading.Tasks;
using VacationRental.Models;

namespace VacationRental.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        public IMapper Mapper;
        public IBookingService _bookingService;
        public IRentalService _rentalService;
        public ICalendarService _calendarService;

        public CalendarController(IMapper mapper, IBookingService bookingService, IRentalService rentalService, ICalendarService calendarService)
        {
            Mapper = mapper;
            _bookingService = bookingService;
            _rentalService = rentalService;
            _calendarService = calendarService;
        }

        [HttpGet]
        public async Task<CalendarViewModel> Get(int rentalId, DateTime start, int nights)
        {
            var response = await _calendarService.GetCalendar(new GetCalendarRequest() { RentalId = rentalId,  BookingStartDate = start,  NumberOfNights = nights });

            if (!response.Succeeded)
            {
                throw new Exception(response.Message);
            }

            return response.CalendarViewModel;
        }
    }
}

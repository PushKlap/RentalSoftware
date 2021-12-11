using AutoMapper;
using DataContext.Models;
using DataContext.UnitOfWork;
using Services.Contracts.Request;
using Services.Contracts.Response;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IBookingService _bookingService;
        private readonly IRentalService _rentalService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CalendarService(IBookingService bookingService, IRentalService rentalService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _bookingService = bookingService;
            _rentalService = rentalService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<GetCalendarResponse> GetCalendar(GetCalendarRequest request)
        {
            GetCalendarResponse response = new GetCalendarResponse();

            try
            {
                if (request.NumberOfNights < 0)
                {
                    response.Message = "Nights must be positive";
                    response.Succeeded = false;
                    return response;
                }

                var rentals = await _rentalService.GetAll();

                var rentals2 = await _unitOfWork.RentalRepository.GetAll(); //Testing Purposes

                if (rentals.Where(w => w.Id == request.RentalId).Count() == 0)
                {
                    response.Message = "Not Found";
                    response.Succeeded = false;
                    return response;
                }

                CalendarViewModel calendarViewModel = new CalendarViewModel
                {
                    RentalId = request.RentalId,
                    Dates = new List<CalendarDateViewModel>()
                };

                for (var i = 0; i < request.NumberOfNights; i++)
                {
                    CalendarDateViewModel calendarDateViewModel = new CalendarDateViewModel
                    {
                        Date = request.BookingStartDate.Date.AddDays(i),
                        Bookings = new List<CalendarBookingViewModel>(),
                        PreparationTimes = new List<PreparationTimesViewModel>()
                    };

                    var rental = await _rentalService.GetByRentalId(request.RentalId);
                    var preparationDays = rental.PreparationTimeInDays;

                    var bookings = await _bookingService.GetAll();
                    bookings = bookings.Where(x => x.Rental.Id == request.RentalId);

                    foreach (var booking in bookings)
                    {

                        if (calendarDateViewModel.Date >= booking.StartDate && calendarDateViewModel.Date < booking.StartDate.AddDays(booking.NumberOfNights))
                        {
                            calendarDateViewModel.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id, Unit = 1 });
                        }

                        if (calendarDateViewModel.Date >= booking.StartDate.AddDays(booking.NumberOfNights) && calendarDateViewModel.Date < booking.StartDate.AddDays(booking.NumberOfNights + preparationDays))
                        {
                            calendarDateViewModel.PreparationTimes.Add(new PreparationTimesViewModel { Unit = 1 });
                        }
                    }

                    calendarViewModel.Dates.Add(calendarDateViewModel);
                }

                response.CalendarViewModel = calendarViewModel;
                response.Succeeded = true;

            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }

    public class BookingsAndRentals
    {
        public int Id { get; set; }
        public Rental Rental { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfNights { get; set; }
    }

}

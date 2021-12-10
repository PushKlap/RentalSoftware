using System;
using AutoMapper;
using DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Services.Contracts.Request;
using Services.Models;
using DataContext.UnitOfWork;

namespace VacationRental.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : Controller
    {
        public IMapper Mapper;
        public IBookingService _bookingService;
        public IRentalService _rentalService;
        public IUnitOfWork _unitOfWork;

        public BookingsController(IMapper mapper, IBookingService bookingService, IRentalService rentalService, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            _bookingService = bookingService;
            _rentalService = rentalService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{bookingId:int}")]
        public async Task<BookingViewModel> Get(int bookingId)
        {
            var response = await _bookingService.GetBooking(new GetBookingRequest() { bookingId = bookingId });

            if (!response.Succeeded)
            {
                throw new Exception(response.Message);
            }

            return response.BookingViewModel;
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(BookingBindingModel model)
        {
            var  response = await _bookingService.AddBooking(new AddBookingRequest() { NumberOfNigths = model.Nights, RentalId = model.RentalId, StartDate = model.Start });

            if (!response.Succeeded)
            {
                throw new ApplicationException(response.Message);
            }

            return response.ResourceIdViewModel;
        }
    }
}

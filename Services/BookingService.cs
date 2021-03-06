using DataContext.Models;
using DataContext.UnitOfWork;
using Services.Contracts.Request;
using Services.Contracts.Response;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentalService _rentalService;

        public BookingService(IUnitOfWork unitOfWork, IRentalService rentalService)
        {
            _unitOfWork = unitOfWork;
            _rentalService = rentalService;
        }

        public async Task<AddBookingResponse> AddBooking(AddBookingRequest request)
        {
            AddBookingResponse response = new AddBookingResponse();

            try
            {
                if (request.NumberOfNigths < 0)
                {
                    response.Message = "Nights must be positive";
                    response.ResourceIdViewModel = new ResourceIdViewModel() { Id = -2 };
                    response.Succeeded = false;
                    return response;
                }

                var rental = await _rentalService.GetByRentalId(request.RentalId); //ERROR HERE

                if (rental == null)
                {
                    response.Message = "Rental not found";
                    response.ResourceIdViewModel = new ResourceIdViewModel() { Id = -2 };
                    response.Succeeded = false;
                    return response;
                }

                for (var i = 0; i < request.NumberOfNigths; i++)
                {
                    var count = 0;

                    foreach (var bookingItem in rental.BookingCollection)
                    {
                        if ((bookingItem.StartDate <= request.StartDate.Date && bookingItem.StartDate.AddDays(bookingItem.NumberOfNights + rental.PreparationTimeInDays) > request.StartDate.Date)
                            || (bookingItem.StartDate < request.StartDate.AddDays(request.NumberOfNigths + rental.PreparationTimeInDays) && bookingItem.StartDate.AddDays(bookingItem.NumberOfNights + rental.PreparationTimeInDays) >= request.StartDate.AddDays(request.NumberOfNigths + rental.PreparationTimeInDays))
                            || (bookingItem.StartDate > request.StartDate && bookingItem.StartDate.AddDays(bookingItem.NumberOfNights + rental.PreparationTimeInDays) < request.StartDate.AddDays(request.NumberOfNigths + rental.PreparationTimeInDays)))
                        {
                            count++;
                        }
                    }

                    var rental2 = await _rentalService.GetByRentalId(request.RentalId);
                    var rentalUnits = rental2.Units;

                    if (count >= rentalUnits) 
                    {
                        response.Message = "Not available";
                        response.ResourceIdViewModel = new ResourceIdViewModel() { Id = -2 };
                        response.Succeeded = false;
                        return response;
                    }
                }

                Booking booking = new Booking()
                {
                    NumberOfNights = request.NumberOfNigths,
                    StartDate = request.StartDate.Date,
                    Rental = rental,
                    RentalId = rental.Id
                };

                await _unitOfWork.BookingRepository.AddAsync(booking);
                await _unitOfWork.Complete();

                response.Succeeded = true;
                response.ResourceIdViewModel = new ResourceIdViewModel() { Id = booking.Id };

            }
            catch (Exception exception)
            {
                response.Succeeded = false;
                response.Message = exception.Message;
                response.ResourceIdViewModel = new ResourceIdViewModel() { Id = -1 };
            }

            return response;
        }

        public async Task<GetBookingResponse> GetBooking(GetBookingRequest request)
        {
            GetBookingResponse response = new GetBookingResponse();

            try
            {
                var booking = await _unitOfWork.BookingRepository.GetByIdAsync(request.bookingId);

                if (booking == null)
                {
                    response.Succeeded = false;
                    response.Message = "Booking not found";
                    return response;
                }

                response.Succeeded = true;

                BookingViewModel bookingViewModel = new BookingViewModel
                {
                    Id = booking.Id,
                    Nights = booking.NumberOfNights,
                    RentalId = booking.Rental.Id,
                    Start = booking.StartDate
                };

                response.BookingViewModel = bookingViewModel;
            }
            catch (Exception exception)
            {
                response.Succeeded = false;
                response.Message = exception.Message;
            }

            return response;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _unitOfWork.BookingRepository.GetAll();
        }
    }
}

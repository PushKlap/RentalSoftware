using DataContext.Models;
using Services.Contracts.Request;
using Services.Contracts.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAll();
        Task<AddBookingResponse> AddBooking(AddBookingRequest request);
        Task<GetBookingResponse> GetBooking(GetBookingRequest request);
    }
}
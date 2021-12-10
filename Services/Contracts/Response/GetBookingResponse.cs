

using DataContext.Models;
using Services.Models;

namespace Services.Contracts.Response
{
    public class GetBookingResponse : ResponseBase
    {
        public BookingViewModel BookingViewModel { get; set; }
    }
}

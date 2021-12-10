using DataContext.Models;

namespace Services.Contracts.Response
{
    public class AddBookingResponse : ResponseBase
    {
        public ResourceIdViewModel ResourceIdViewModel { get; set; }
    }
}

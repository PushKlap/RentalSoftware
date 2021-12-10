

using DataContext.Models;

namespace Services.Contracts.Response
{
    public class AddRentalResponse : ResponseBase
    {
        public ResourceIdViewModel ResourceIdViewModel { get; set; }
    }
}

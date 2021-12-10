
using DataContext.Models;
using Services.Models;

namespace Services.Contracts.Response
{
    public class GetRentalResponse : ResponseBase
    {
        public RentalViewModel RentalViewModel { get; set; }
    }
}

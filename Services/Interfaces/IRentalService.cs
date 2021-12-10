using DataContext.Models;
using Services.Contracts.Request;
using Services.Contracts.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRentalService
    {
        Task<List<Rental>> GetAll();
        Task<Rental> GetByRentalId(int rentalId);
        Task<AddRentalResponse> AddRental(AddRentalRequest request);
        Task<GetRentalResponse> GetRental(GetRentalRequest request);
        Task<UpdateRentalResponse> UpdateRental(UpdateRentalRequest request);
    }
}
using DataContext.Models;
using System.Threading.Tasks;

namespace DataContext.Repositories.Interfaces
{
    public interface IRentalRepository : IRepositoryBase<Rental>
    {
        Task<Rental> GetRentalById(int rentalId);
    }
}
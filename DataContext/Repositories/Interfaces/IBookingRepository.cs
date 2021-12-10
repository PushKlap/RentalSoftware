using DataContext.Models;
using System.Threading.Tasks;

namespace DataContext.Repositories.Interfaces
{
    public interface IBookingRepository : IRepositoryBase<Booking>
    {
        Task<Booking> GetByIdAsync(int Id);
    }
}
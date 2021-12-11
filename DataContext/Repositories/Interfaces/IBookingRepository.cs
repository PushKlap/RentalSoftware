using DataContext.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataContext.Repositories.Interfaces
{
    public interface IBookingRepository : IRepositoryBase<Booking>
    {
        Task<Booking> GetByIdAsync(int Id);
        Task<List<Booking>> GetAllAsync();
    }
}
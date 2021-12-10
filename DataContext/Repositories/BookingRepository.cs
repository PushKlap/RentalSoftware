using DataContext.Models;
using DataContext.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataContext.Repositories
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(ApiContext context) : base(context) { }

        public async Task<Booking> GetByIdAsync(int Id)
        {
            return await Context.Bookings
                    .Where(x => x.Id == Id)
                    .FirstOrDefaultAsync();
        }
    }
}

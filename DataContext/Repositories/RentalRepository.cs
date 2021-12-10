using DataContext.Models;
using DataContext.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataContext.Repositories
{
    public class RentalRepository : RepositoryBase<Rental>, IRentalRepository
    {
        public RentalRepository(ApiContext context) : base(context) { }

        public async Task<Rental> GetRentalById(int rentalId)
        {
            return await Context.Rentals
                    .Where(x => x.Id == rentalId).AsNoTracking()
                    .FirstOrDefaultAsync();
        }
    }
}

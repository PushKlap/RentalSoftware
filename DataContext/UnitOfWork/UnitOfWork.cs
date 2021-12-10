using DataContext.Repositories;
using DataContext.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace DataContext.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _context;

        public IRentalRepository RentalRepository { get; }

        public IBookingRepository BookingRepository { get; }

        public UnitOfWork(ApiContext context)
        {
            _context = context;
            RentalRepository = new RentalRepository(_context);
            BookingRepository = new BookingRepository(_context);
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

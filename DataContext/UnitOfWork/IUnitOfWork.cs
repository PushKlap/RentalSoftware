using DataContext.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DataContext.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBookingRepository BookingRepository { get; }
        IRentalRepository RentalRepository { get; }

        Task<int> Complete();
        void Dispose();
    }
}
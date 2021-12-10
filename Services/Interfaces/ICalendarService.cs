using Services.Contracts.Request;
using Services.Contracts.Response;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICalendarService
    {
        Task<GetCalendarResponse> GetCalendar(GetCalendarRequest request);
    }
}
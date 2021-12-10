using Services.Models;

namespace Services.Contracts.Response
{
    public class GetCalendarResponse : ResponseBase
    {
        public CalendarViewModel CalendarViewModel { get; set; }
    }
}

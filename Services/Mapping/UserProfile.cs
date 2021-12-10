using AutoMapper;
using DataContext.Models;
using Services.Models;

namespace Services.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Booking, BookingViewModel>();

            CreateMap<BookingViewModel, Booking>();

            CreateMap<Rental, RentalViewModel>();

            CreateMap<RentalBindingModel, Rental>();

            CreateMap<RentalViewModel, Rental>();
            
            CreateMap<BookingBindingModel, Booking>();
        }
    }
}

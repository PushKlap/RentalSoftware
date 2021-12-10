using AutoMapper;
using DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Request;
using Services.Interfaces;
using Services.Models;
using System;
using System.Threading.Tasks;
using VacationRental.Models;

namespace VacationRental.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        public IMapper Mapper;
        public IBookingService _bookingService;
        public IRentalService _rentalService;

        public RentalsController(IMapper mapper, IBookingService bookingService, IRentalService rentalService)
        {
            Mapper = mapper;
            _bookingService = bookingService;
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public async Task<RentalViewModel> Get(int rentalId)
        {
            var rental = await _rentalService.GetRental(new GetRentalRequest() { RentalId = rentalId });

            if (rental == null)
            {
                throw new ApplicationException("Not Found");
            }

            return rental.RentalViewModel;
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        public async Task<IActionResult> Put(int rentalId, RentalBindingModel model)
        {
            //Use Automapper Here
            UpdateRentalRequest updateRentalRequest = new UpdateRentalRequest() { Id = rentalId, Units = model.Units, PreparationTimeInDays = (int)model.PreparationTimeInDays };

            var response = await _rentalService.UpdateRental(updateRentalRequest);

            if (!response.Succeeded)
            {
                return new JsonResult(response.Errors);
            }

            return new JsonResult(response.Succeeded);
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(RentalBindingModel model)
        {
            //Use Automapper Here
            var response = await _rentalService.AddRental(new AddRentalRequest() { PreparationTimeInDays = Convert.ToInt32(model.PreparationTimeInDays), Units = model.Units });

            if (!response.Succeeded)
            {
                throw new Exception(response.Message);
            }

            return response.ResourceIdViewModel;
        }
    }
}

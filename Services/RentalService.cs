using DataContext.Enums;
using DataContext.Models;
using DataContext.UnitOfWork;
using Services.Contracts.Request;
using Services.Contracts.Response;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<UpdateRentalResponse> UpdateRental(UpdateRentalRequest request)
        {
            UpdateRentalResponse response = new UpdateRentalResponse();

            Rental rental = await _unitOfWork.RentalRepository.Get(request.Id);

            if (rental.BookingCollection.Count() == 0 && (rental.PreparationTimeInDays == request.PreparationTimeInDays && rental.Units < request.Units))
            {
                rental.PreparationTimeInDays = request.PreparationTimeInDays;
                rental.Units = request.Units;
                response.Succeeded = true;
                return response;
            }

            // The request should fail if overlapping between existing bookings and/ or their preparation times occurs due to a decrease of the number of units or an increase of the length of preparation time.
            // If the length of preparation time is changed then it should be updated for all existing bookings
            foreach (var item in rental.BookingCollection)
            {
                // Yet to do           
            }

            return response;
        }

        public async Task<Rental> GetByRentalId(int rentalId)
        {
            var rental = await _unitOfWork.RentalRepository.GetRentalById(rentalId);

            var bookings = await _unitOfWork.BookingRepository.GetAllAsync();
            bookings = bookings.Where(x => x.Rental.Id == rental.Id).ToList();

            rental.BookingCollection = bookings.Where(x => x.Rental.Id == rentalId).ToList(); //ERROR HERE

            return rental;
        }


        public async Task<List<Rental>> GetAll()
        {
            return await _unitOfWork.RentalRepository.GetAll();
        }

        public async Task<GetRentalResponse> GetRental(GetRentalRequest request)
        {
            GetRentalResponse response = new GetRentalResponse();

            try
            {
                var rental = await _unitOfWork.RentalRepository.Get(request.RentalId);

                if (rental == null)
                {
                    response.Message = "Rental not found";
                    response.Succeeded = false;
                    return response;
                }

                response.Succeeded = true;

                RentalViewModel rentalViewModel = new RentalViewModel
                {
                    Id = rental.Id,
                    PreparationTimeInDays = rental.PreparationTimeInDays,
                    Units = rental.Units
                };

                response.RentalViewModel = rentalViewModel; 

            }
            catch (Exception exception)
            {
                response.Succeeded = false;
                response.Message = exception.Message;
            }

            return response;
        }
        public async Task<AddRentalResponse> AddRental(AddRentalRequest request)
        {
            AddRentalResponse response = new AddRentalResponse();

            try
            {
                Rental tmpRental = new Rental()
                {
                    PreparationTimeInDays = request.PreparationTimeInDays,
                    RentalType = RentalTypeEnum.Unknown,
                    Units = request.Units
                };

                await _unitOfWork.RentalRepository.AddAsync(tmpRental);
                await _unitOfWork.Complete();

                response.Succeeded = true;
                response.ResourceIdViewModel = new ResourceIdViewModel() { Id = tmpRental.Id };
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = ex.Message;
                response.ResourceIdViewModel = new ResourceIdViewModel() { Id = -1 };
            }

            return response;
        }
    }
}

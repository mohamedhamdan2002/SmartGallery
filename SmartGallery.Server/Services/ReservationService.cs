using SendGrid.Helpers.Errors.Model;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReservationViewModels;
using System.Linq.Expressions;

namespace SmartGallery.Server.Services;

public class ReservationService : IReservationService
{
    private readonly IRepositoryManager _repository;
    private readonly IUserService _userService;

    public ReservationService(IRepositoryManager repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<IEnumerable<ReservationDetailsVM>> GetReservationsAsync(bool trackChanges = false)
    {
        var reservations = await _repository.Reservation.GetReservationsAsync(
            x  => new
            {
                x.CustomerId,
                x.Customer.Email,
                x.ServiceId,
                x.Service.Name,
                x.ProblemDescription,
                x.ReservationDate,
                x.ReservationTime,
                x.Status,
            }, 
            trackChanges: false,
            "Customer", "Service"
        );
        var reservationsViewModel = reservations.Select(x => new ReservationDetailsVM(
            x.ProblemDescription,
            x.Status,
            x.ReservationDate,
            x.ReservationTime,
            x.CustomerId,
            x.Email!,
            x.ServiceId,
            x.Name
        ));
        return reservationsViewModel;
    }
    public async Task<ReservationViewModel> GetReservationAsync(int serviceId, string customerId, bool trackChanges = false)
    {
        var reservation = await GetReservationAndCheckIfItExistAsync(serviceId, customerId, trackChanges);
        
        return reservation.ToViewModel();
    }
    public async Task<ReservationViewModel> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationViewModel reservationForCreationViewModel)
    {
        await CheckIfServiceExistsAsync(serviceId);
        await CheckIfCustomerExistsAsync(customerId);
        var reservationEntity = ToReservation(serviceId, customerId, reservationForCreationViewModel);
        await _repository.Reservation.CreateAsync(reservationEntity);
        await _repository.SaveChangesAsync();
        return reservationEntity.ToViewModel();
    }

    public async Task UpdateReservationAsync(int serviceId, string customerId, ReservationForUpdateViewModel reservationForUpdateViewModel)
    {
        var reservation = await GetReservationAndCheckIfItExistAsync(serviceId, customerId, trackChanges: true);
        reservation.ProblemDescription = reservationForUpdateViewModel.ProblemDescription!;
        reservation.ReservationTime = reservationForUpdateViewModel.ReservationTime;
        reservation.ReservationDate = reservationForUpdateViewModel.ReservationDate;
        reservation.Status = reservationForUpdateViewModel.Status;
        await _repository.SaveChangesAsync();

    }
    private Reservation ToReservation(int serviceId, string customerId, ReservationForCreationViewModel reservationForCreationViewModel)
    {
        return new Reservation
        {
            CustomerId = customerId,
            ServiceId = serviceId,
            ProblemDescription = reservationForCreationViewModel.ProblemDescription!,
            ReservationDate = reservationForCreationViewModel.ReservationDate,
            ReservationTime = reservationForCreationViewModel.ReservationTime
        };
    }
    private async Task<Reservation> GetReservationAndCheckIfItExistAsync(int serviceId, string customerId, bool trackChanges = false, params string[] includeProperties)
    {
        var reservation = await _repository.Reservation.GetReservationAsync(serviceId, customerId, trackChanges, includeProperties);
        if(reservation is null)
            throw new NotFoundException($"the Reservation with serviceId: {serviceId} and customerId {customerId} doesn't exist in the database.");
        return reservation;
    }

    private async Task CheckIfServiceExistsAsync(int serviceId)
    {
        var isExist = await _repository.Service.CheckIfServiceExistAsync(serviceId);
        if(!isExist)
            throw new NotFoundException($"the service with id: {serviceId} doesn't exist in the database.");
    }
    private async Task CheckIfCustomerExistsAsync(string customerId)
    {
        var isExist = await _userService.CheckIfUserExistByIdAsync(customerId);
        if(!isExist)
            throw new NotFoundException($"the customer with id: {customerId} doesn't exist in the database.");
    }

    public async Task<IEnumerable<ReservationCustomerDetailsVM>> GetReservationsForServiceAsync(int serviceId)
    {
        await CheckIfServiceExistsAsync(serviceId);
        var reservations = await _repository.Reservation
            .FindReservationsAsync(
                reservation => reservation.ServiceId == serviceId,
                trackChanges: false,
                selector: x => new
                {
                    x.CustomerId,
                    x.Customer.PhoneNumber,
                    x.Customer.Email,
                    x.Customer.Address,
                    x.ProblemDescription,
                    x.ReservationDate,
                    x.ReservationTime,
                    x.Status,
                },
                includeProperties: "Customer"
            );
        List<ReservationCustomerDetailsVM> reservationsViewModel = new();
        if(reservations is not null && reservations.Any())
        {
            reservationsViewModel = reservations.Select(
                x => new ReservationCustomerDetailsVM(
                    x.ProblemDescription,
                    x.Status,
                    x.ReservationDate,
                    x.ReservationTime,
                    x.CustomerId,
                    x.Email!,
                    x.PhoneNumber!,
                    x.Address!
                 )).ToList();
        }
        return reservationsViewModel;
    }

    public async Task<IEnumerable<ReservationServiceDetailsVM>> GetReservationsForCustomerAsync(string customerId)
    {
        await CheckIfCustomerExistsAsync(customerId);
        var reservations = await _repository.Reservation
            .FindReservationsAsync(
                reservation => reservation.CustomerId == customerId,
                selector: x => new
                {
                    x.ServiceId,
                    x.Service.Name,
                    x.Service.Description,
                    x.ProblemDescription,
                    x.ReservationDate,
                    x.ReservationTime,
                    x.Status,
                },
                trackChanges: false,
                includeProperties: "Service"
            );
        List<ReservationServiceDetailsVM> reservationsViewModel = new();
        if(reservations is not null && reservations.Any())
        {
            reservationsViewModel = reservations.Select(
                x => new ReservationServiceDetailsVM(
                    x.ProblemDescription,
                    x.Status,
                    x.ReservationDate,
                    x.ReservationTime,
                    x.ServiceId,
                    x.Name
                 )).ToList();
                        
        }
        return reservationsViewModel;
    }


}

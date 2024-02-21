using SendGrid.Helpers.Errors.Model;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

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
    public async Task<IEnumerable<ReservationViewModel>> GetReservationsAsync(bool trackChanges = false)
    {
        var reservations = await _repository.Reservation.GetReservationsAsync(trackChanges);
        var reservationsViewModel = reservations.Select(reservation => reservation.ToViewModel());
        return reservationsViewModel;
    }
    public async Task<ReservationViewModel> GetReservationAsync(int serviceId, string customerId, bool trackChanges = false, params string[] includeProperties)
    {
        var reservation = await GetReservationAndCheckIfItExistAsync(serviceId, customerId, trackChanges, includeProperties);
        // should handle case if includeProperties
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

    public async Task UpdateReservationAsync(int serviceId, string customerId, ReservationForUpdateViewModel reservationForUpdateViewModel, bool trackChanges = false)
    {
        var reservation = await GetReservationAndCheckIfItExistAsync(serviceId, customerId, trackChanges);
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

}

using Microsoft.AspNetCore.Http.HttpResults;
using SmartGallery.Server.Exceptions;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Server.Utilities;
using SmartGallery.Shared.ViewModels.ItemViewModels;
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
    public async Task<IEnumerable<ReservationDetailsVM>> GetReservationsAsync()
    {
        var reservationsViewModel = await _repository.Reservation.GetReservationsAsync(
            x => new ReservationDetailsVM(
                x.Id,
                x.ProblemDescription,
                x.Status,
                x.ReservationDate,
                x.ReservationTime,
                x.CustomerId,
                x.Customer.Email!,
                x.ServiceId,
                x.Service.Name,
                x.ItemId,
                x.Item.Name
            ),
            trackChanges: false,
            NavigationProperties.Customer,
            NavigationProperties.Service,
            NavigationProperties.Item
        );
        return reservationsViewModel;
    }
    public async Task<ReservationDetailsVM> GetReservationByIdAsync(int id)
    {
        var reservation = await GetReservationAndCheckIfItExistAsync(
            id,
            x => new ReservationDetailsVM(
                id,
                x.ProblemDescription,
                x.Status,
                x.ReservationDate,
                x.ReservationTime,
                x.CustomerId,
                x.Customer.Email!,
                x.ServiceId,
                x.Service.Name,
                x.ItemId,
                x.Item.Name
            ),
            trackChanges: false,
            NavigationProperties.Customer,
            NavigationProperties.Service, 
            NavigationProperties.Item);

        return reservation;
    }
    public async Task<IEnumerable<ReservationDetailsVM>> GetReservationAsync(int serviceId, string customerId)
    {
        var reservation = await _repository.Reservation.FindReservationsAsync(
           reservation => reservation.ServiceId == serviceId && reservation.CustomerId == customerId,
            x => new ReservationDetailsVM(
                x.Id,
                x.ProblemDescription,
                x.Status,
                x.ReservationDate,
                x.ReservationTime,
                x.CustomerId,
                x.Customer.Email!,
                x.ServiceId,
                x.Service.Name,
                x.ItemId,
                x.Item.Name
            ),
            trackChanges: false,
            NavigationProperties.Customer,
            NavigationProperties.Service,
            NavigationProperties.Item
            );

        return reservation;
    }
    public async Task<IEnumerable<ReservationCustomerDetailsVM>> GetReservationsForServiceAsync(int serviceId)
    {
        await CheckIfServiceExistsAsync(serviceId);
        var reservationsViewModel = await _repository.Reservation
            .FindReservationsAsync(
                reservation => reservation.ServiceId == serviceId,
                selector: x => new ReservationCustomerDetailsVM(
                    x.Id,
                    x.ProblemDescription,
                    x.Status,
                    x.ReservationDate,
                    x.ReservationTime,
                    x.CustomerId,
                    x.Customer.Email!,
                    x.Customer.PhoneNumber!,
                    x.Customer.Address!, 
                    x.ItemId,
                    x.Item.Name
                 ),
                trackChanges:false,
                 NavigationProperties.Customer,
                 NavigationProperties.Item

            );
        return reservationsViewModel;
    }

    public async Task<IEnumerable<ReservationServiceDetailsVM>> GetReservationsForCustomerAsync(string customerId)
    {
        await CheckIfCustomerExistsAsync(customerId);
        var reservationsViewModel = await _repository.Reservation
            .FindReservationsAsync(
                reservation => reservation.CustomerId == customerId,
                selector: x => new ReservationServiceDetailsVM(
                    x.Id,
                    x.ProblemDescription,
                    x.Status,
                    x.ReservationDate,
                    x.ReservationTime,
                    x.ServiceId,
                    x.Service.Name,
                    x.ItemId,
                    x.Item.Name
                 ),
                trackChanges: false,
                NavigationProperties.Service,
                NavigationProperties.Item
            );
        return reservationsViewModel;
    }

    public async Task<ReservationViewModel> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationViewModel reservationForCreationViewModel)
    {
        await CheckIfServiceExistsAsync(serviceId);
        await CheckIfCustomerExistsAsync(customerId);
        var reservationEntity = new Reservation
        {
            CustomerId = customerId,
            ServiceId = serviceId,
            ItemId = reservationForCreationViewModel.ItemId,
            ProblemDescription = reservationForCreationViewModel.ProblemDescription!,
            ReservationDate = reservationForCreationViewModel.ReservationDate,
            ReservationTime = reservationForCreationViewModel.ReservationTime
        };
        await _repository.Reservation.CreateAsync(reservationEntity);
        await _repository.SaveChangesAsync();
        return reservationEntity.ToViewModel();
    }

    public async Task UpdateReservationAsync(int id, ReservationForUpdateViewModel reservationForUpdateViewModel)
    {
        var reservation = await GetReservationAndCheckIfItExistAsync(
            id,
            reservation => reservation,
            trackChanges: true
            );
        reservation.ProblemDescription = reservationForUpdateViewModel.ProblemDescription!;
        reservation.ReservationTime = reservationForUpdateViewModel.ReservationTime;
        reservation.ReservationDate = reservationForUpdateViewModel.ReservationDate;
        reservation.Status = reservationForUpdateViewModel.Status;
        await _repository.SaveChangesAsync();

    }
    private async Task<TResult> GetReservationAndCheckIfItExistAsync<TResult>(int id, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
    {
        var reservation = await _repository.Reservation.GetReservationByIdAsync(id, selector, trackChanges, includeProperties);
        if (reservation is null)
            throw new NotFoundException($"the Reservation with id : {id} doesn't exist in the database.");
        return reservation;
    }

    private async Task CheckIfServiceExistsAsync(int serviceId)
    {
        var isExist = await _repository.Service.CheckIfServiceExistAsync(serviceId);
        if (!isExist)
            throw new NotFoundException($"the service with id: {serviceId} doesn't exist in the database.");
    }
    private async Task CheckIfCustomerExistsAsync(string customerId)
    {
        var isExist = await _userService.CheckIfUserExistByIdAsync(customerId);
        if (!isExist)
            throw new NotFoundException($"the customer with id: {customerId} doesn't exist in the database.");
    }
    //private async Task CheckIfReservationExistsAsync(int serviceId, string customerId)
    //{
    //    var isExist = await _repository.Reservation.CheckIfReservationExistAsync(serviceId, customerId);
    //    if (isExist)
    //        throw new ConflictException($"A reservation with the same details already exists.");

    //}
 
}
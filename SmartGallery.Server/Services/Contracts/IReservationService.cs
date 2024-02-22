using SmartGallery.Server.Models;
using SmartGallery.Shared.ViewModels.ReservationViewModels;
using System.Linq.Expressions;

namespace SmartGallery.Server.Services.Contracts;

public interface IReservationService
{
    Task<IEnumerable<ReservationDetailsVM>> GetReservationsAsync();
    Task<IEnumerable<ReservationCustomerDetailsVM>> GetReservationsForServiceAsync(int serviceId);
    Task<IEnumerable<ReservationServiceDetailsVM>> GetReservationsForCustomerAsync(string customerId);
    Task<ReservationViewModel> GetReservationAsync(int serviceId, string customerId);
    Task<ReservationViewModel> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationViewModel reservationForCreationViewModel);
    Task UpdateReservationAsync(int serviceId, string customerId, ReservationForUpdateViewModel reservationForUpdateViewModel);
}

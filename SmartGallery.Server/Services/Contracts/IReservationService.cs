using SmartGallery.Server.Models;
using SmartGallery.Shared.ViewModels.ReservationViewModels;
using System.Linq.Expressions;

namespace SmartGallery.Server.Services.Contracts;

public interface IReservationService
{
    Task<IEnumerable<ReservationDetailsVM>> GetReservationsAsync();
    Task<IEnumerable<ReservationCustomerDetailsVM>> GetReservationsForServiceAsync(int serviceId);
    Task<IEnumerable<ReservationDetailsVM>> GetReservationAsync(int serviceId, string customerId);
    Task<IEnumerable<ReservationServiceDetailsVM>> GetReservationsForCustomerAsync(string customerId);
    Task<ReservationDetailsVM> GetReservationByIdAsync(int id);
    Task<ReservationViewModel> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationViewModel reservationForCreationViewModel);
    Task UpdateReservationAsync(int id, ReservationForUpdateViewModel reservationForUpdateViewModel);
    Task DeleteReservationAsync(int id);
}

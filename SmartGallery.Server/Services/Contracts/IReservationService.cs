using SmartGallery.Server.Models;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Server.Services.Contracts;

public interface IReservationService
{
    Task<IEnumerable<ReservationViewModel>> GetReservationsAsync(bool trackChanges = false);
    Task<ReservationViewModel> GetReservationAsync(int serviceId, string customerId, bool trackChanges = false, params string[] includeProperties);
    Task<ReservationViewModel> CreateReservationAsync(int serviceId, string customerId, ReservationForCreationViewModel reservationForCreationViewModel);
    Task UpdateReservationAsync(int serviceId, string customerId, ReservationForUpdateViewModel reservationForUpdateViewModel, bool trackChanges = false);
}

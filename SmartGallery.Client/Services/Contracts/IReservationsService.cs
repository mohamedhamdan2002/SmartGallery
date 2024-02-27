using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Client.Services.Contracts;

public interface IReservationsService
{
    Task<IEnumerable<ReservationServiceDetailsVM>?> GetReservationsForCustomerByCustomerId(string id);
    Task<IEnumerable<ReservationCustomerDetailsVM>?> GetReservationsForServiceByServiceId(int id);
    Task<ReservationDetailsVM?> GetReservationByIdAsync(int id);
    Task<IEnumerable<ReservationDetailsVM>> GetReservationAsync(int serviceId, string customerId);
    Task UpdateReservationAsync(int id, ReservationForUpdateViewModel reservationForUpdateViewModel);
    Task<ReservationForCreationViewModel?> CreateReservation(int serviceId, string customerId,ReservationForCreationViewModel reservationForCreationViewModel);
    Task DeleteReservation(string customerId, int serviceid);
}


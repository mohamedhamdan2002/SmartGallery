using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Client.Services.Contracts;

public interface IReservationsService
{
    Task<IEnumerable<ReservationServiceDetailsVM>?> GetReservationsForCustomerByCustomerId(string id);
    Task<IEnumerable<ReservationViewModel>?> GetReservationsForServiceByServiceId(int id);
    Task<ReservationForCreationViewModel?> GetReservation(int serviceId, string customerId);
    Task<ReservationForUpdateViewModel?> UpdateReservation(int serviceId, string customerId, ReservationForUpdateViewModel reservationForUpdateViewModel);

    Task<ReservationForCreationViewModel?> CreateReservation(int serviceId, string customerId,ReservationForCreationViewModel reservationForCreationViewModel);
    Task DeleteReservation(string customerId, int serviceid);
}


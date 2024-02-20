using SmartGallery.Server.Models;
namespace SmartGallery.Server.Repositories.Contracts;

public interface IReservationRepository : IRepository
{
    Task<IEnumerable<Reservation>> GetReservationsAsync(bool trackChanges = false);
    Task<Reservation?> GetReservationAsync(int serviceId, string customerId, bool trackChanges = false, params string[] includeProperties);
    Task CreateReservationAsync(Reservation reservation);
    void DeleteReservation(Reservation reservation);
}

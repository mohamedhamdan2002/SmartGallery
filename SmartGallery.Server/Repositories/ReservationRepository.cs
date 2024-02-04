using Microsoft.EntityFrameworkCore;
using SmartGallery.Server.Data;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;

namespace SmartGallery.Server.Repositories;

public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(AppDbContext context)
        : base(context) {}

    public async Task CreateReservationAsync(Reservation reservation)
        => await CreateAsync(reservation);
    public void DeleteReservation(Reservation reservation)
        => Delete(reservation);

    public async Task<Reservation?> GetReservationAsync(int serviceId, string customerId, bool trackChanges = false, params string[] includeProperties)
        => await GetByCondition(reservation =>
            reservation.ServiceId == serviceId &&
            reservation.CustomerId == customerId,
            trackChanges, includeProperties)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Reservation>> GetReservationsAsync(bool trackChanges = false)
        => await GetAll(trackChanges)
                .ToListAsync();
}

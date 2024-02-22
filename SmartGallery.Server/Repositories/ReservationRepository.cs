using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Server.Data;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;

namespace SmartGallery.Server.Repositories;

public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(AppDbContext context)
        : base(context) {}


    public async Task<IEnumerable<TResult>> FindReservationsAsync<TResult>(Expression<Func<Reservation, bool>> predicate, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
        => await GetByCondition(predicate, trackChanges, includeProperties)
            .Select(selector)
            .ToListAsync();

    public async Task<Reservation?> GetReservationAsync(int serviceId, string customerId, bool trackChanges = false, params string[] includeProperties)
        => await GetByCondition(reservation =>
            reservation.ServiceId == serviceId &&
            reservation.CustomerId == customerId,
            trackChanges, includeProperties)
            .SingleOrDefaultAsync();
    public async Task<TResult?> GetReservationAsync<TResult>(int serviceId, string customerId, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
    => await GetByCondition(reservation =>
        reservation.ServiceId == serviceId &&
        reservation.CustomerId == customerId,
        trackChanges, includeProperties)
        .Select(selector)
        .SingleOrDefaultAsync();

    public async Task<IEnumerable<TResult>> GetReservationsAsync<TResult>(Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
          => await GetAll(trackChanges, includeProperties).Select(selector).ToListAsync();

    public Task<IEnumerable<Reservation>> GetReservationsAsync(bool trackChanges = false)
    {
        throw new NotImplementedException();
    }
}

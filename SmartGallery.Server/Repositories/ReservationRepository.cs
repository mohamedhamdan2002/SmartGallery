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

    public async Task<bool> CheckIfReservationExistAsync(int serviceId, string customerId)
       => await CheckIfExistByConditionAsync(x => x.ServiceId == serviceId && x.CustomerId == customerId);
    public async Task<bool> CheckIfReservationExistAsync(int id)
        => await CheckIfExistByConditionAsync(x => x.Id == id);
    public async Task<IEnumerable<TResult>> FindReservationsAsync<TResult>(Expression<Func<Reservation, bool>> predicate, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
        => await GetByCondition(predicate, trackChanges, includeProperties)
            .Select(selector)
            .ToListAsync();

    public async Task<TResult?> GetReservationByIdAsync<TResult>(int id, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
    {
        var query = GetByCondition(reservation =>
            reservation.Id == id,
            trackChanges, includeProperties);
            return await query.Select(selector).SingleOrDefaultAsync(); 
    }
    public async Task<TResult?> GetReservationAsync<TResult>(int serviceId, string customerId, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
    {
        var query = GetByCondition(reservation =>
            reservation.ServiceId == serviceId &&
            reservation.CustomerId == customerId,
            trackChanges, includeProperties);
        return await query.Select(selector).SingleOrDefaultAsync();
    }
    public async Task<IEnumerable<TResult>> GetReservationsAsync<TResult>(Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
          => await GetAll(trackChanges, includeProperties).Select(selector).ToListAsync();

    public async Task DeleteReservationAsync(Reservation reservation) => Delete(reservation);
}

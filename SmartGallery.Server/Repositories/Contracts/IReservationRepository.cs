using System.Linq.Expressions;
using SmartGallery.Server.Models;
namespace SmartGallery.Server.Repositories.Contracts;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetReservationsAsync(bool trackChanges = false);
    Task<Reservation?> GetReservationAsync(int serviceId, string customerId, bool trackChanges = false, params string[] includeProperties);
    Task<IEnumerable<TResult>> GetReservationsAsync<TResult>(Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties);
    Task<IEnumerable<TResult>> FindReservationsAsync<TResult>(Expression<Func<Reservation, bool>> predicate, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties);
    Task<TResult?> GetReservationAsync<TResult>(int serviceId, string customerId, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties);
}

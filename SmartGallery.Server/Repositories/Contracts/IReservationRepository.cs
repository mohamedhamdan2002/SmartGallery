using System.Linq.Expressions;
using SmartGallery.Server.Models;
namespace SmartGallery.Server.Repositories.Contracts;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<bool> CheckIfReservationExistAsync(int serviceId, string customerId);
    Task<IEnumerable<TResult>> GetReservationsAsync<TResult>(Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties);
    Task<IEnumerable<TResult>> FindReservationsAsync<TResult>(Expression<Func<Reservation, bool>> predicate, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties);
    Task<TResult?> GetReservationByIdAsync<TResult>(int id, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties);
    Task<TResult?> GetReservationAsync<TResult>(int serviceId, string customerId, Expression<Func<Reservation, TResult>> selector, bool trackChanges = false, params string[] includeProperties);
    Task<bool> CheckIfReservationExistAsync(int id);
}

using System.Linq.Expressions;
using SmartGallery.Server.Models;
namespace SmartGallery.Server.Repositories.Contracts;

public interface IReviewRepository : IRepository<Review>
{
    Task<IEnumerable<TResult>> FindReviewsAsync<TResult>(
            Expression<Func<Review, bool>> predicate, 
            Expression<Func<Review, TResult>> selector, 
            bool trackChanges = false, 
            params string[] includeProperties
        );
    Task<IEnumerable<TResult>> GetReviewsAsync<TResult>(
            Expression<Func<Review, TResult>> selector,
            bool trackChanges = false,
            params string[] includeProperties
        );
}
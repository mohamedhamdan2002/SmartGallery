using Microsoft.EntityFrameworkCore;
using SmartGallery.Server.Data;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using System.Linq.Expressions;

namespace SmartGallery.Server.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) 
            : base(context) {}
        public async Task<IEnumerable<TResult>> FindReviewsAsync<TResult>(Expression<Func<Review, bool>> predicate, Expression<Func<Review, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
            => await GetByCondition(predicate, trackChanges, includeProperties)
                    .Select(selector)
                    .ToListAsync();
        public async Task<IEnumerable<TResult>> GetReviewsAsync<TResult>(Expression<Func<Review, TResult>> selector, bool trackChanges = false, params string[] includeProperties)
            => await GetAll(trackChanges, includeProperties).Select(selector).ToListAsync();

    }
}

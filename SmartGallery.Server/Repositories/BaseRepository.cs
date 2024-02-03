using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Server.Data;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;

namespace SmartGallery.Server.Repositories;

public abstract class BaseRepository<T> : IRepository where T : BaseEntity
{
    protected readonly AppDbContext _context;
    public BaseRepository(AppDbContext context)
        => _context = context;
    protected async Task CreateAsync(T entity)
        => await _context.AddAsync(entity);
    protected void Delete(T entity)
        => _context.Remove(entity);
    protected IQueryable<T> GetAll(bool trackChanges = false)
        => !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();
    protected IQueryable<T> GetByCondition(Expression<Func<T, bool>> predicate, bool trackChanges = false, params string[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>().Where(predicate);
        if(includeProperties.Any())
            foreach (var property in includeProperties)
                query = query.Include(property);
        if(!trackChanges)
            query = query.AsNoTracking();
        return query;
    }
}
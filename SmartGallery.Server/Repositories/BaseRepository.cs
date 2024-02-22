using System;
using System.Linq.Expressions;
using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using SmartGallery.Server.Data;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;

namespace SmartGallery.Server.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    public BaseRepository(AppDbContext context)
        => _context = context;
    public async Task CreateAsync(T entity)
        => await _context.AddAsync(entity);
    public void Delete(T entity)
        => _context.Remove(entity);
    protected IQueryable<T> GetAll(bool trackChanges = false, params string[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includeProperties.Any())
            foreach (var property in includeProperties)
                query = query.Include(property);
        if (!trackChanges)
            query = query.AsNoTracking();
        return query;
    }
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
    public async Task<bool> CheckIfExistByConditionAsync(Expression<Func<T, bool>> predicate)
        => await _context.Set<T>().AnyAsync(predicate);
    public void Update(T entity)
        => _context.Entry(entity).State = EntityState.Modified;
    
}
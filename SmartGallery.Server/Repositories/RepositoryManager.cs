﻿using SmartGallery.Server.Data;
using SmartGallery.Server.Repositories.Contracts;

namespace SmartGallery.Server.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AppDbContext _context;

    public RepositoryManager(IServiceProvider serviceProvider, AppDbContext context)
    {
        _serviceProvider = serviceProvider;
        _context = context;
    }

    public IServiceRepository Service => GetRepository<IServiceRepository>();

    public IReservationRepository Reservation => GetRepository<IReservationRepository>();

    private TRepository GetRepository<TRepository>() where TRepository : IRepository
        => (TRepository) _serviceProvider.GetRequiredService(typeof(TRepository));

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}


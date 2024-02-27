using Microsoft.EntityFrameworkCore;
using SmartGallery.Server.Data;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;

namespace SmartGallery.Server.Repositories;

public class ServiceRepository : BaseRepository<Service>, IServiceRepository
{
    public ServiceRepository(AppDbContext context)
        : base(context) { }

    public async Task<bool> CheckIfServiceExistAsync(int id)
        => await CheckIfExistByConditionAsync(service => service.Id == id);
    public async Task<Service?> GetServiceByIdAsync(int id, bool trackChanges = false, params string[] includeProperties)
        => await GetByCondition(service => service.Id == id, trackChanges, includeProperties)
                .FirstOrDefaultAsync();
    public async Task<IEnumerable<Service>> GetServicesAsync(bool trackChanges = false)
        => await GetAll(trackChanges)
                .OrderBy(service => service.Name)
                .ToListAsync();
    public async Task DeleteServiceAsync(Service service) => Delete(service);
}

using SmartGallery.Server.Models;
namespace SmartGallery.Server.Repositories.Contracts;

public interface IServiceRepository : IRepository<Service>
{
    Task<IEnumerable<Service>> GetServicesAsync(bool trackChanges = false);
    Task<Service?> GetServiceByIdAsync(int id, bool trackChanges = false, params string [] includeProperties);
    Task<bool> CheckIfServiceExistAsync(int id);
}

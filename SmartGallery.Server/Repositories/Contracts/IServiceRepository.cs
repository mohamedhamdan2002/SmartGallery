using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;

namespace SmartGallery.Server;

public interface IServiceRepository : IRepository
{
    Task<IEnumerable<Service>> GetServicesAsync(bool trackChanges = false);
    Task<Service?> GetServiceByIdAsync(int id, bool trackChanges = false, params string [] includeProperties);
    Task CreateServiceAsync(Service service);
    void DeleteService(Service service);
    Task<bool> CheckIfServiceExistAsync(int id);
}

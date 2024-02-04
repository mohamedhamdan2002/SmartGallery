using SmartGallery.Server.Models;
namespace SmartGallery.Server.Repositories.Contracts;

public interface IServiceRepository : IRepository
{
    Task<IEnumerable<Service>> GetServicesAsync(bool trackChanges = false);
    Task<Service?> GetServiceByIdAsync(int id, bool trackChanges = false, params string [] includeProperties);
    Task CreateServiceAsync(Service service);
    void DeleteService(Service service);
    Task<bool> CheckIfServiceExistAsync(int id);
}

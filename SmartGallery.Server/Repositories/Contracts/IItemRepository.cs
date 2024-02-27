using SmartGallery.Server.Models;
namespace SmartGallery.Server.Repositories.Contracts;

public interface IItemRepository : IRepository<Item>
{
    Task<IEnumerable<Item>> GetItemsForServiceAsync(int serviceId, bool trackChanges = false);
    Task<Item?> GetItemForServiceByIdAsync(int serviceId, int id, bool trackChanges = false, params string[] includeProperties);
}
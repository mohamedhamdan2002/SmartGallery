using Microsoft.EntityFrameworkCore;
using SmartGallery.Server.Data;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;

namespace SmartGallery.Server.Repositories;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    public ItemRepository(AppDbContext context) 
        : base(context)
    {
    }
    public async Task<Item?> GetItemForServiceByIdAsync(int serviceId, int id, bool trackChanges = false, params string[] includeProperties)
        => await GetByCondition(item => item.ServiceId == serviceId, trackChanges, includeProperties).FirstOrDefaultAsync();
    public async Task<IEnumerable<Item>> GetItemsForServiceAsync(int serviceId, bool trackChanges = false)
        => await GetByCondition(item => item.ServiceId == serviceId, trackChanges).ToListAsync();
}
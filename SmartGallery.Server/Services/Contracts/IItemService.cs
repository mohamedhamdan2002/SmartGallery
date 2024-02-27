using SmartGallery.Shared.ViewModels.ItemViewModels;

namespace SmartGallery.Server.Services.Contracts;

public interface IItemService
{
    Task<IEnumerable<ItemViewModel>> GetItemsForServiceAsync(int serviceId, bool trackChanges = false);
    Task<ItemViewModel> CreateItemForServiceAsync(int serviceId, ItemCreateUpdateViewModel model);
    Task UpdateItemForServiceAsync(int serviceId, int id, ItemCreateUpdateViewModel model);
    Task DeleteItemForServiceAsync(int serviceId, int id, bool trackChanges = false);
}
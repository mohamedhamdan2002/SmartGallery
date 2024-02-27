using SmartGallery.Shared.ViewModels.ItemViewModels;

namespace SmartGallery.Client.Services.Contracts;

public interface IServiceItemsService
{
    Task<IEnumerable<ItemViewModel>> GetItemsForServiceAsync(int serviceId);
    Task CreateItemForServiceAsync(int serviceId, ItemCreateUpdateViewModel model);
    Task UpdateItemForServiceAsync(int serviceId, int id, ItemCreateUpdateViewModel model);
    Task DeleteItemForServiceAsync(int serviceId, int id);
}


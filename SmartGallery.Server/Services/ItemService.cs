using SmartGallery.Server.Exceptions;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ItemViewModels;

namespace SmartGallery.Server.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepositoryManager _repository;


        public ItemService(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public async Task<ItemViewModel> CreateItemForServiceAsync(int serviceId, ItemCreateUpdateViewModel model)
        {
            await CheckIfServiceExistsAsync(serviceId);
            var item = new Item
            {
                ServiceId = serviceId,
                Name = model.Name,
            };
            await _repository.Item.CreateAsync(item);
            await _repository.SaveChangesAsync();
            var itemViewModel = new ItemViewModel(item.Id, item.Name);
            return itemViewModel;
        }

        public async Task DeleteItemForServiceAsync(int serviceId, int id, bool trackChanges = false)
        {
            var item = await GetItemAndCheckIfItExistAsync(serviceId, id, trackChanges);
            _repository.Item.Delete(item);
            await _repository.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<ItemViewModel>> GetItemsForServiceAsync(int serviceId, bool trackChanges = false)
        {
            await CheckIfServiceExistsAsync(serviceId);
            var items = await _repository.Item.GetItemsForServiceAsync(serviceId, trackChanges: false);
            return items.Select(x => new ItemViewModel(x.Id, x.Name));
        }

        public async Task UpdateItemForServiceAsync(int serviceId, int id, ItemCreateUpdateViewModel model)
        {
            await CheckIfServiceExistsAsync(serviceId);
            var item = await GetItemAndCheckIfItExistAsync(serviceId, id, trackChanges: true);
            item.Name = model.Name;
            await _repository.SaveChangesAsync();
        }
        private async Task CheckIfServiceExistsAsync(int serviceId)
        {
            var isExist = await _repository.Service.CheckIfServiceExistAsync(serviceId);
            if (!isExist)
                throw new NotFoundException($"the service with id: {serviceId} doesn't exist in the database.");
        }
        private async Task<Item> GetItemAndCheckIfItExistAsync(int serviceId, int id, bool trackChanges = false, params string[] includeProperties)
        {
            var item = await _repository.Item.GetItemForServiceByIdAsync(serviceId, id, trackChanges, includeProperties);
            if (item is null)
                throw new NotFoundException($"the item with id: {id} doesn't exist in the database.");
            return item;
        }
    }
}

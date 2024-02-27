using System.Text.Json;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ItemViewModels;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Client.Services
{
	public class ServiceItemsService:IServiceItemsService
	{
        private readonly HttpClient _httpClient;

        public ServiceItemsService(HttpClient httpClient)
		{
            _httpClient = httpClient;
        }

        public async Task CreateItemForServiceAsync(int serviceId, ItemCreateUpdateViewModel model)
        {
            await _httpClient.PostAsJsonAsync($"api/services/{serviceId}/Items", model);
        }

        public async Task DeleteItemForServiceAsync(int serviceId, int id)
        {
            await _httpClient.DeleteAsync($"api/services/{serviceId}/Items/{id}");
        }

        public async Task<IEnumerable<ItemViewModel>> GetItemsForServiceAsync(int serviceId)
        {
            var Services = await JsonSerializer.DeserializeAsync<IEnumerable<ItemViewModel>>
                                      (await _httpClient.GetStreamAsync($"api/services/{serviceId}/Items"), new JsonSerializerOptions()
                                      { PropertyNameCaseInsensitive = true }) ?? new List<ItemViewModel>();
            return Services;
        }

        public async Task UpdateItemForServiceAsync(int serviceId, int id, ItemCreateUpdateViewModel model)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/services/{serviceId}/Items/{id}", model);
        }
    }
}


using System.Net.Http;
using System.Text.Json;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Client.Services;

public class ServicesService : IServicesService
{
    private readonly HttpClient _httpClient;

    public ServicesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateService(ServiceForCreationViewModel serviceForCreationViewModel)
    {
        await _httpClient.PostAsJsonAsync("api/Services", serviceForCreationViewModel);
    }

    public async Task DeleteService(int id)
    {
        await _httpClient.DeleteAsync($"api/Services/{id}");
    }

    public async Task<ServiceViewModel?> GetServiceById(int id)
    {
        var ServiceObj = await _httpClient.GetStreamAsync($"api/Services/{id}");
        if (ServiceObj is not null)
        {
            var Service = await JsonSerializer.DeserializeAsync<ServiceViewModel>
            (ServiceObj, new JsonSerializerOptions()
            { PropertyNameCaseInsensitive = true });
            return Service!;
        }
        return null;

    }

    public async Task<IEnumerable<ServiceViewModel>?> GetServices()
    {
        var Services = await JsonSerializer.DeserializeAsync<IEnumerable<ServiceViewModel>>
                           (await _httpClient.GetStreamAsync("api/Services"), new JsonSerializerOptions()
                           { PropertyNameCaseInsensitive = true }) ?? new List<ServiceViewModel>();
        return Services;

    }

    public async Task<ServiceForUpdateViewModel?> UpdateService(int id, ServiceForUpdateViewModel serviceForUpdateViewModel)
    {
        var result = await _httpClient.PutAsJsonAsync($"api/Services/{id}", serviceForUpdateViewModel);
        if (result.IsSuccessStatusCode)
            return serviceForUpdateViewModel;
        else
            return null;
    }
}


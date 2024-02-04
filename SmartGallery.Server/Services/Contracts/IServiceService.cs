using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Server.Services.Contracts;

public interface IServiceService
{
    Task<IEnumerable<ServiceViewModel>> GetServicesAsync(bool trackChanges = false);
    Task<ServiceViewModel> GetServiceByIdAsync(int id, bool trackChanges = false, params string[] includeProperties);
    Task<ServiceViewModel> CreateServiceAsync(ServiceForCreationViewModel serviceForCreationViewModel);
    Task UpdateServiceAsync(int id, ServiceForUpdateViewModel serviceForUpdateViewModel, bool trackChanges = false);
    Task DeleteServiceAsync(int id, bool trackChanges = false);
}

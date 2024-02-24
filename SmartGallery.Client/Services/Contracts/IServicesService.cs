using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Client.Services.Contracts;

public interface IServicesService
{
    Task<IEnumerable<ServiceViewModel>?> GetServices();
    Task<ServiceViewModel?> GetServiceById(int id);
    Task<ServiceForUpdateViewModel?> UpdateService(int id ,ServiceForUpdateViewModel serviceForUpdateViewModel);
    Task CreateService(ServiceForCreationViewModel serviceForCreationViewModel);
    Task DeleteService(int id);
}


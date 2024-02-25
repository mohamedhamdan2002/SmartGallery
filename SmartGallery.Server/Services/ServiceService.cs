using SendGrid.Helpers.Errors.Model;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Server.Services;

public class ServiceService : IServiceService
{
    private readonly IRepositoryManager _repository;


    public ServiceService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<ServiceViewModel> CreateServiceAsync(ServiceForCreationViewModel serviceForCreationViewModel)
    {
        var serviceEntity = ToService(serviceForCreationViewModel);
        await _repository.Service.CreateAsync(serviceEntity);
        await _repository.SaveChangesAsync();
        return serviceEntity.ToViewModel();

    }

    public async Task DeleteServiceAsync(int id, bool trackChanges = false)
    {
        Service service = await GetServiceAndCheckIfItExistAsync(id);
        _repository.Service.Delete(service);
        await _repository.SaveChangesAsync();
    }

    public async Task<ServiceViewModel> GetServiceByIdAsync(int id, bool trackChanges = false, params string[] includeProperties)
    {
        var serviceEntity = await GetServiceAndCheckIfItExistAsync(id, trackChanges);
        return serviceEntity.ToViewModel();
    }

    public async Task<IEnumerable<ServiceViewModel>> GetServicesAsync(bool trackChanges = false)
    {
        var services = await _repository.Service.GetServicesAsync(trackChanges);
        var servicesViewModel = services.Select(service => service.ToViewModel());
        return servicesViewModel;
    }

    public async Task UpdateServiceAsync(int id, ServiceForUpdateViewModel serviceForUpdateViewModel, bool trackChanges = false)
    {
        var serviceEntity = await GetServiceAndCheckIfItExistAsync(id, trackChanges);
        serviceEntity.Name = serviceForUpdateViewModel.Name!;
        serviceEntity.Description = serviceForUpdateViewModel.Description!;
        await _repository.SaveChangesAsync();
    }
    private Service ToService(ServiceForCreationViewModel serviceForCreationViewModel)
    {
        return new Service
        {
            Name = serviceForCreationViewModel.Name!,
            Description = serviceForCreationViewModel.Description!
        };
    }
    private async Task<Service> GetServiceAndCheckIfItExistAsync(int id, bool trackChanges = false, params string[] includeProperties)
    {
        var service = await _repository.Service.GetServiceByIdAsync(id, trackChanges, includeProperties);
        if(service is null)
            throw new NotFoundException($"the service with id: {id} doesn't exist in the database.");
        return service;
    }
}
using SendGrid.Helpers.Errors.Model;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Server.Services;

public class ServiceService : IServiceService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IRepositoryManager repository)
    {
        _repositoryManager = repository;
        _serviceRepository = repository.GetRepository<IServiceRepository>();
    }

    public async Task<ServiceViewModel> CreateServiceAsync(ServiceForCreationViewModel serviceForCreationViewModel)
    {
        var serviceEntity = ToService(serviceForCreationViewModel);
        await _serviceRepository.CreateServiceAsync(serviceEntity);
        await _repositoryManager.SaveChangesAsync();
        var serviceViewModel = ToServiceViewModel(serviceEntity);
        return serviceViewModel;
    }

    public Task DeleteServiceAsync(int id, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceViewModel> GetServiceByIdAsync(int id, bool trackChanges = false, params string[] includeProperties)
    {
        var serviceEntity = await GetServiceAndCheckIfItExistAsync(id, trackChanges);
        var serviceViewModel  = ToServiceViewModel(serviceEntity);
        return serviceViewModel;
    }

    public async Task<IEnumerable<ServiceViewModel>> GetServicesAsync(bool trackChanges = false)
    {
        var services = await _serviceRepository.GetServicesAsync(trackChanges);
        var servicesViewModel = services.Select(service => ToServiceViewModel(service));
        return servicesViewModel;
    }

    public async Task UpdateServiceAsync(int id, ServiceForUpdateViewModel serviceForUpdateViewModel, bool trackChanges = false)
    {
        var serviceEntity = await GetServiceAndCheckIfItExistAsync(id, trackChanges);
        serviceEntity.Name = serviceForUpdateViewModel.Name!;
        serviceEntity.Description = serviceForUpdateViewModel.Description!;
        await _repositoryManager.SaveChangesAsync();
    }
    private ServiceViewModel ToServiceViewModel(Service service)
    {
        return new ServiceViewModel(
            Id: service.Id,
            Name: service.Name,
            Description: service.Description
        );
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
        var service = await _serviceRepository.GetServiceByIdAsync(id, trackChanges, includeProperties);
        if(service is null)
            throw new NotFoundException($"the service with id: {id} doesn't exist in the database.");
        return service;
    }
//     appDbContext.Entry(student).State = EntityState.Modified;
// await _appDbContext.SaveChangesAsync();
}
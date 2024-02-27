using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SmartGallery.Client.Services;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReservationViewModels;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Client.Pages.Services;

public partial class ServicesPage
{
    [Parameter]
    public int serviceId { get; set; }
    public string? MessageToShow { get; set; }
    bool isSuccess { get; set; }
    bool isFailed { get; set; }
    public ServiceForCreationViewModel viewModel { get; set; } = new();
    [Inject] IServicesService _servicesService { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] ILoginService _loginService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        if (serviceId > 0)
        {
            ServiceViewModel? serviceViewModel = await _servicesService.GetServiceById(serviceId);
            if (serviceViewModel is not null)
            {
                viewModel.Icon = serviceViewModel.Icon;
                viewModel.Description = serviceViewModel.Description;
                viewModel.Name = serviceViewModel.Name;
            }
        }
        await base.OnInitializedAsync();
    }
    public async Task HandleValidSubmitAsync()
    {
        isSuccess = false;

        if (serviceId == 0)
        {
            await _servicesService.CreateService(viewModel);
            MessageToShow = "Service Created Successfully";
            _navigationManager.NavigateTo("/admin/services");

        }
        else
        {
            ServiceViewModel? serviceViewModel = await _servicesService.GetServiceById(serviceId);
            ServiceForUpdateViewModel serviceForUpdateViewModel = new();
            serviceForUpdateViewModel.Icon = serviceViewModel.Icon;
            serviceForUpdateViewModel.Description = serviceViewModel.Description;
            serviceForUpdateViewModel.Name = serviceViewModel.Name;

            await _servicesService.UpdateService(serviceId, serviceForUpdateViewModel);
            MessageToShow = "Service Updated Successfully";
            _navigationManager.NavigateTo("/admin/services");
        }
        isSuccess = true;
        await InvokeAsync(StateHasChanged);
    }
    private async Task LogoutAsync()
    {
        await _loginService.LogoutAsync();
        _navigationManager.NavigateTo("/");
    }
    private void NavigateToHome()
    {
        _navigationManager.NavigateTo("/");
    }
}


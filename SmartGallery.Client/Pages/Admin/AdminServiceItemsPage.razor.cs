using Microsoft.AspNetCore.Components;
using SmartGallery.Client.Services.Contracts;

namespace SmartGallery.Client.Pages.Admin;

public partial class AdminServiceItemsPage
{

    [Inject] ILoginService _loginService { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
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


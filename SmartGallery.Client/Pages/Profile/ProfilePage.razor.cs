using Microsoft.AspNetCore.Components;
using SmartGallery.Client.Services.Contracts;

namespace SmartGallery.Client.Pages.Profile;

public partial class ProfilePage
{
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] ILoginService _loginService { get; set; }
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


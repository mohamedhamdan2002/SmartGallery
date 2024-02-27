using System;
using Microsoft.AspNetCore.Components;
using SmartGallery.Client.Services.Contracts;

namespace SmartGallery.Client.Shared
{
	public partial class CustomMainLayout
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
}


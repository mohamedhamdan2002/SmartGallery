using Microsoft.AspNetCore.Components;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels;

namespace SmartGallery.Client.Pages.Admin
{
	public partial class AdminUsersPage
	{
        List<CustomerViewModel> CustomerViewModels { get; set; } = new();
		[Inject]
		ILoginService _loginService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            CustomerViewModels = await _loginService.GetUsers() ?? new();
            await base.OnInitializedAsync();
        }
        public async Task NavigateToReservation(string userId)
        {
            navigationManager.NavigateTo($"/Profile/{userId}");
        }
    }
}


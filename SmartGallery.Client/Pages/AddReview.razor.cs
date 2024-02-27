using Microsoft.AspNetCore.Components;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReviewViewModels;

namespace SmartGallery.Client.Pages
{
	public partial class AddReview
	{
        private int selectedVal = 0;
        private int? activeVal;


        private string LabelText => (activeVal ?? selectedVal) switch
        {
            1 => "Very bad",
            2 => "Bad",
            3 => "Sufficient",
            4 => "Good",
            5 => "Awesome!",
            _ => "Rate our product!"
        };

        public bool isSuccess { get; set; }
        public ReviewForCreationVM viewModel { get; set; } = new();
        [Inject]public  ILoginService _loginService { get; set; }
        [Inject]public NavigationManager _navigationManager { get; set; }
        private async Task LogoutAsync()
        {
            await _loginService.LogoutAsync();
            _navigationManager.NavigateTo("/");
        }
        private void NavigateToHome()
        {
            _navigationManager.NavigateTo("/");
        }
        private async Task HandleValidSubmitAsync()
        {
            viewModel.Rating = selectedVal;
        }

        private void HandleHoveredValueChanged(int? val) => activeVal = val;


    }
}


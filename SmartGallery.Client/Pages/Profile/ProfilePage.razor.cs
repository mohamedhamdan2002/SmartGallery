using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Client.Pages.Profile;

public partial class ProfilePage
{
    [Parameter]
    public string? userId { get; set; }

    public List<ReservationServiceDetailsVM> reservationsViewModel { get; set; } = new();
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] ILoginService _loginService { get; set; }
    [Inject] IReservationsService _reservationsService { get; set; }
    [Inject] AuthenticationStateProvider _authenticationStateProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if(userId is null)
        { 
            var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.User;
            userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }
        reservationsViewModel = (await _reservationsService.GetReservationsForCustomerByCustomerId(userId))?.ToList() ?? new();

        await base.OnInitializedAsync();
    }
   

    private async Task DeleteReservation(int serviceId)
    {
        await _reservationsService.DeleteReservation(userId, serviceId);
        reservationsViewModel = (await _reservationsService.GetReservationsForCustomerByCustomerId(userId))?.ToList() ?? new();
        await InvokeAsync(StateHasChanged);
    }
    private void NavigateToReservationEdit(int Id)
    {
        _navigationManager.NavigateTo($"/ReservationEdit/{Id}");
    }
    private void NavigateToReview(int Id)
    {
        _navigationManager.NavigateTo($"/AddReview/{Id}");
    }

}


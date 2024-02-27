using Microsoft.AspNetCore.Components;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Client.Pages.Admin;

public partial class AdminReservationsPage
{
    [Parameter]
    public int serviceId { get; set; }
    public List<ReservationCustomerDetailsVM> reservationsViewModel { get; set; } = new();
    [Inject] IReservationsService _reservationsService { get; set; }
    [Inject] NavigationManager navigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        reservationsViewModel = (await _reservationsService.GetReservationsForServiceByServiceId(serviceId))?.ToList() ?? new();

        await base.OnInitializedAsync();
    }
    private async Task NavigateToReservationEdit(int serviceId,string customerId)
    {
        navigationManager.NavigateTo($"/ReservationEdit/{serviceId}/{customerId}");
    }
    private async Task DeleteReservation(int serviceId, string customerId)
    {
        await _reservationsService.DeleteReservation(customerId, serviceId);
        reservationsViewModel = (await _reservationsService.GetReservationsForServiceByServiceId(serviceId))?.ToList() ?? new();
        await InvokeAsync(StateHasChanged);
    }
}


﻿using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Client.Pages.Reservations;

public partial class ReservationsPage
{
    [Parameter]
    public int serviceId { get; set; }
    [Parameter]
    public int ReservationId { get; set; }

    bool isSuccess { get; set; }
    bool isFailed { get; set; }
    public ReservationForCreationViewModel viewModel { get; set; } = new();
    [Inject] IReservationsService _reservationsService { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] AuthenticationStateProvider _authenticationStateProvider { get; set;}
    public string MessageToShow { get; set; } = " ";
    public async Task HandleValidSubmitAsync()
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authenticationState.User;
        string userId = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

        var response = await _reservationsService.CreateReservation(serviceId,userId,viewModel);
        if (response is not null)
        {
            isSuccess = true;
            MessageToShow = "Your Reservation Was Done Successfully";
            await InvokeAsync(StateHasChanged);
            await Task.Delay(TimeSpan.FromSeconds(1.5));
            _navigationManager.NavigateTo("/");
        }
        else
        {
            isFailed = true;
            MessageToShow = "You Already Done A Reservation If you want something else Please Add it to your Old Reservation";
        }
        await InvokeAsync(StateHasChanged);
    }

}


using System;
using Microsoft.AspNetCore.Components;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared;

namespace SmartGallery.Client.Pages;

public partial class Register
{
    private RegisterViewModel viewModel = new RegisterViewModel();
    private bool ShowErrors;
    private IEnumerable<string>? Errors;
    private string? Error { get; set; }
    [Inject]
    public NavigationManager _navigationManager { get; set; }
    [Inject]
    public ILoginService _loginService { get; set; }
    private async Task HandleRegistration()
    {
        ShowErrors = false;

        var result = await _loginService.RegisterAsync(viewModel);

        if (result.IsSuccess)
        {
            _navigationManager.NavigateTo("/login");
        }
        else
        {
            Errors = result.Errors;
            ShowErrors = true;
            Error = result.Message;
        }
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


using System;
using SmartGallery.Shared;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using SmartGallery.Client.Services.Contracts;
using Microsoft.JSInterop;

namespace SmartGallery.Client.Pages;

public partial class Login
{
   
    private LoginViewModel viewModel = new LoginViewModel();
    private List<string> validationMessages = new List<string>();
    [Inject]
    public ILoginService _loginService { get; set; }
    [Inject]
    public NavigationManager _navigationManager { get; set; }
    public string Error { get; set; }
    public bool ShowErrors { get; set; }
    private async Task HandleValidSubmitAsync()
    {
        ShowErrors = false;
        await _loginService.LogoutAsync();
        UserManagerResponse Response = await _loginService.LoginAsync(viewModel);
        validationMessages.Clear();
        if (!Response.IsSuccess)
        {
            ShowErrors = true;
            Error = Response.Message ?? " ";
            validationMessages.Add(Response.Message);
        }
        else
        {
            _navigationManager.NavigateTo("/");
        }
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


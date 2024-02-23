using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SmartGallery.Client.Services;
using SmartGallery.Client.Services.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartGallery.Client.Pages;

public partial class Index
{
    private bool IsUserLogin { get; set; } = false;
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] ILoginService _loginService { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }
    public void ShowLoginForm()
    {
        IsUserLogin = true;
        Console.WriteLine(IsUserLogin);
    }
    private void NavigateToLogin()
    {
        _navigationManager.NavigateTo("Login");
    }
    private async Task LogoutAsync()
    {
        await _loginService.LogoutAsync();
        _navigationManager.NavigateTo("/");
    }
    private async Task showbar()
    {
        await JSRuntime.InvokeVoidAsync("showbar");
    }

}


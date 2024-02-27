using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SmartGallery.Client.Services;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ServiceViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartGallery.Client.Pages;

public partial class Index
{
    private bool IsUserLogin { get; set; } = false;
    public List<ServiceViewModel> services { get; set; } = new();
    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] ILoginService _loginService { get; set; }
    [Inject] IServicesService _servicesService { get; set; }
    [Inject] IJSRuntime JSRuntime { get; set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JSRuntime.InvokeVoidAsync("initSwiper", null);
    }
    protected override async Task OnInitializedAsync()
    {
        services = (await _servicesService.GetServices()).ToList();
        await InvokeAsync(StateHasChanged);
        await base.OnInitializedAsync();
    }
    public void ShowLoginForm()
    {
        IsUserLogin = true;
        Console.WriteLine(IsUserLogin);
    }
    private void NavigateToLogin()
    {
        _navigationManager.NavigateTo("Login");
    }
    private void NavigateToProfile()
    {
        _navigationManager.NavigateTo("Profile");
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
    async Task ScrollToSection(string sectionId)
    {
        await JSRuntime.InvokeVoidAsync("scrollToSection", sectionId);
    }
}


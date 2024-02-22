using System;
using Microsoft.AspNetCore.Components;

namespace SmartGallery.Client.Pages;

public partial class Index
{
    private bool IsUserLogin { get; set; } = false;
    [Inject] NavigationManager navigationManager { get; set; }
    private void HandleLoginChildValueChanged(bool newValue)
    {
        IsUserLogin = newValue;
    }
    public void ShowLoginForm()
    {
        IsUserLogin = true;
        Console.WriteLine(IsUserLogin);
    }
    private void NavigateToLogin()
    {
        navigationManager.NavigateTo("Login");
    }
}


using System;
namespace SmartGallery.Client.Pages;

public partial class Index
{
    private bool IsUserLogin { get; set; } = false; 
    private void HandleLoginChildValueChanged(bool newValue)
    {
        IsUserLogin = newValue;
    }
    public void ShowLoginForm()
    {
        IsUserLogin = true;
        Console.WriteLine(IsUserLogin);
    }
}


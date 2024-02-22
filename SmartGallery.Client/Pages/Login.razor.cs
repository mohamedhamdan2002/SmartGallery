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
    private async Task HandleValidSubmitAsync()
    {
        UserManagerResponse Response = await _loginService.LoginAsync(viewModel);

        _navigationManager.NavigateTo("/");
        await InvokeAsync(StateHasChanged);
    }

    private async Task HandleInValidSubmitAsync()
    {
        validationMessages.Clear();
        var validationContext = new ValidationContext(viewModel, null, null);
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(viewModel, validationContext, validationResults, true))
        {
            foreach (ValidationResult result in validationResults)
            {
                validationMessages.Add(result.ErrorMessage);
            }
        }
        await InvokeAsync(StateHasChanged);
    }
}


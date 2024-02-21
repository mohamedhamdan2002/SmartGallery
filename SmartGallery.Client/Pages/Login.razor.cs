//using System;
//using SmartGallery.Shared;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Components;
//using SmartGallery.Client.Services.Contracts;
//using Microsoft.JSInterop;

//namespace SmartGallery.Client.Pages;

//public partial class Login
//{
//    //[Parameter]
//    //public bool IsUserLogin { get; set; }
//    //[Parameter]
//    //public EventCallback<bool> UserLoginChanged { get; set; }

//    private LoginViewModel viewModel = new LoginViewModel();
//    private List<string> validationMessages = new List<string>();
//    [Inject]
//    public ILoginService loginService { get; set; }
//    private async Task HandleValidSubmitAsync()
//    {
//        UserManagerResponse Response = await loginService.LoginAsync(viewModel);

//        await InvokeAsync(StateHasChanged);
//    }

//    private async Task HandleInValidSubmitAsync()
//    {
//        validationMessages.Clear();
//        var validationContext = new ValidationContext(viewModel, null, null);
//        var validationResults = new List<ValidationResult>();
//        if (!Validator.TryValidateObject(viewModel, validationContext, validationResults, true))
//        {
//            foreach (ValidationResult result in validationResults)
//            {
//                validationMessages.Add(result.ErrorMessage);
//            }
//        }
//        await InvokeAsync(StateHasChanged);
//    }
//}


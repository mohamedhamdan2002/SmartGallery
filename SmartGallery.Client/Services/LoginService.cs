using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SmartGallery.Client.Helpers;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared;
using SmartGallery.Shared.ViewModels;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Client.Services;

public class LoginService : ILoginService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _LocalStorageService;

    public LoginService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _LocalStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
    }
    public async Task<UserManagerResponse> LoginAsync(LoginViewModel loginModel)
    {
        var loginAsJson = JsonSerializer.Serialize(loginModel);

            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

            var loginResult = JsonSerializer.Deserialize<UserManagerResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult!;
            }

            await _LocalStorageService.SetItemAsync("authToken", loginResult!.Message);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResult.Message!);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Message);

            return loginResult;


    }
    //public async Task<UserManagerResponse> LoginAsync(LoginViewModel viewModel)
    //{
    //    var LoginObj = new StringContent(JsonSerializer.Serialize(viewModel), Encoding.UTF8, "application/json");

    //    HttpResponseMessage responseMessage = await _httpClient.PostAsync("api/Auth/Login", LoginObj);
    //    UserManagerResponse result = await responseMessage.Content.ReadFromJsonAsync<UserManagerResponse>() ?? new();

    //    return result;
    //}
    public async Task<UserManagerResponse> RegisterAsync(RegisterViewModel registerModel)
    {
        var result = await _httpClient.PostAsJsonAsync("api/Auth/Register", registerModel);
        if (!result.IsSuccessStatusCode)
            return new UserManagerResponse { IsSuccess = false,Message="You Entered An Already Used Email Please Try Another One", Errors = null };
        return new UserManagerResponse { IsSuccess = true, Errors = new List<string> { "Error occured" } };
    }
    public async Task LogoutAsync()
    {
        await _LocalStorageService.RemoveItemAsync("authToken");
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
    public async Task<List<CustomerViewModel>?> GetUsers()
    {
        Stream? CustomersStream = await _httpClient.GetStreamAsync($"api/Auth/Users");
        if (CustomersStream is not null)
        {
            List<CustomerViewModel> ReservationsForCustomer = await JsonSerializer.DeserializeAsync<List<CustomerViewModel>>(CustomersStream, new JsonSerializerOptions()
            { PropertyNameCaseInsensitive = true }) ?? new List<CustomerViewModel>();
            return ReservationsForCustomer;
        }
        return null;
    }
}


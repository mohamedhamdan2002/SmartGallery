using SmartGallery.Shared;
using SmartGallery.Shared.ViewModels;

namespace SmartGallery.Client.Services.Contracts;

public interface ILoginService
{
    Task<UserManagerResponse> LoginAsync(LoginViewModel viewModel);
    Task<UserManagerResponse> RegisterAsync(RegisterViewModel registerModel);
    Task LogoutAsync();
    Task<List<CustomerViewModel>?> GetUsers();
}


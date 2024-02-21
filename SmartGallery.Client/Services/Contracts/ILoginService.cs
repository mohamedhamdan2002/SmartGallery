using SmartGallery.Shared;

namespace SmartGallery.Client.Services.Contracts;

public interface ILoginService
{
    Task<UserManagerResponse> LoginAsync(LoginViewModel viewModel);
    public Task<UserManagerResponse> RegisterAsync(RegisterViewModel registerModel);
    public Task LogoutAsync();

}


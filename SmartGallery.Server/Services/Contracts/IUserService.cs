using SmartGallery.Shared;

namespace SmartGallery.Server.Services.Contracts;

public interface IUserService
{
    Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
    Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
    Task<UserManagerResponse> LogoutUserAsync();
    Task<bool> CheckIfUserExistByIdAsync(string userId);
}
using SmartGallery.Shared;

namespace SmartGallery.Server.Services;

public interface IUserService
{
    Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
    Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
}



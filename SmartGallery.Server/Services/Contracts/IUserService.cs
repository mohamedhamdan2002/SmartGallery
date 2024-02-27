using SmartGallery.Server.Models;
using SmartGallery.Shared;
using SmartGallery.Shared.ViewModels;

namespace SmartGallery.Server.Services.Contracts;

public interface IUserService
{
    List<CustomerViewModel>? GetAllUsers();
    Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
    Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
    Task<UserManagerResponse> LogoutUserAsync();
    Task<bool> CheckIfUserExistByIdAsync(string userId);
}
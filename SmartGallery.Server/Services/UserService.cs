using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SmartGallery.Server.Models;
using SmartGallery.Shared;

namespace SmartGallery.Server.Services;

public class UserService : IUserService
{
    private readonly SignInManager<Customer> _signInManager;
    private readonly UserManager<Customer> _userManager;
    private readonly IConfiguration _configuration;

    public UserService( UserManager<Customer> userManager , SignInManager<Customer> signInManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
        _signInManager = signInManager;
    }

    public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
    {
        if(model == null)
            throw new NullReferenceException("Register Model is Null");

        if (model.Password != model.ConfirmPassword)
            return new UserManagerResponse
            {
                Message = "Confirm Password doesn't Match Password",
                IsSuccess = false
            };

        var identityUser = new Customer()
        {
            Email = model.Email,
            UserName = model.Email,
            Address = model.Address
        };
        var result = await _userManager.CreateAsync(identityUser,model.Password);
        if(result.Succeeded)
        {
            return new UserManagerResponse
            {
                Message = "User Created Successfully ",
                IsSuccess = true,
            };
        }
        return new UserManagerResponse
        {
            Message = "User Didn't Create Successfully",
            IsSuccess = false,
            Errors = result.Errors.Select(e => e.Description)
        };
    }

    public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
            model.RememberMe, false);

        var user = _userManager.FindByEmailAsync(model.Email);

        if (!result.Succeeded)
        {
            return new UserManagerResponse
            {
                Message = "Invalid Login Attempt",
                IsSuccess = false,
            };
        }


        var claims = new[]
         {
                new Claim("Email", model.Email),
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            };

        var keyBuffer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[Constants.Key]));

        var token = new JwtSecurityToken(issuer: _configuration[Constants.Issuer],
            audience: _configuration[Constants.Audience],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: new SigningCredentials(keyBuffer, SecurityAlgorithms.HmacSha256)
            );

        string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        return new UserManagerResponse
        {
            Message = tokenAsString,
            IsSuccess = true,
            ExpireDate = token.ValidTo
        };

    }

    public async Task<UserManagerResponse> LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
        return new UserManagerResponse
        {
            Message = "User Logged Out Successfully",
            IsSuccess = true,
        };
    }
}



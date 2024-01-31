using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SmartGallery.Shared;

namespace SmartGallery.Server.Services;

public class UserService : IUserService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserService( UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager,IConfiguration configuration)
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

        var identityUser = new IdentityUser()
        {
            Email = model.Email,
            UserName = model.Email,
        };
        var result = await _userManager.CreateAsync(identityUser, model.Password);
        if(result.Succeeded)
        {
            //TODO: Send a Confirmation Message
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

        if (result.Succeeded)
        {
            var claims = new[]
            {
            new Claim("Email",model.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id)
            };

            var keyBuffer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
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
    }
}



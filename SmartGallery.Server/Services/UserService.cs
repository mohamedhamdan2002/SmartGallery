using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using SmartGallery.Server.Models;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared;
using SmartGallery.Shared.ViewModels;

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
            await _userManager.AddToRoleAsync(identityUser, "User");
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
        Customer user = new();
        try { 
        user = await _userManager.FindByEmailAsync(model.Email) ?? new();
        }
        catch(Exception ex)
        {
            Console.WriteLine("Exception", ex.Message);
        }
        if (!result.Succeeded)
        {
            return new UserManagerResponse
            {
                Message = "Invalid Login Attempt",
                IsSuccess = false,
            };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
                new Claim("Email", model.Email),
               new Claim(ClaimTypes.Name,user.Email.ToString()),
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
        };

        foreach(var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
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
        try
        {
            await _signInManager.SignOutAsync();
            return new UserManagerResponse
            {
                Message = "User Logged Out Successfully",
                IsSuccess = true,
            };
        }
        catch(Exception ex)

        {
            return new UserManagerResponse
            {
                Message = "Fatal Error",
                IsSuccess = true,
                Errors = new List<string> { ex.Data.ToString() }
            };
        }
    }

    public async Task<bool> CheckIfUserExistByIdAsync(string userId)
        => await _userManager.Users.AnyAsync(user => user.Id == userId);


    public List<CustomerViewModel>? GetAllUsers()
    {
        List<Customer> users = _userManager.Users.ToList();
        List<CustomerViewModel> Customers = CustomerToCustomerViewModel(users);
        if (users == null)
        {
            return null;
        }

        return Customers;
    }
    public List<CustomerViewModel> CustomerToCustomerViewModel(List<Customer> users)
    {
        List<CustomerViewModel> Customers = new();
        foreach(Customer customer in users)
        {
            Customers.Add(new() { Id = customer.Id, Address = customer.Address, Email = customer.Email });
        }
        return Customers;
    }
}
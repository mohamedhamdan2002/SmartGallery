using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Server.Services;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared;

namespace SmartGallery.Server.Controllers;
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    //private readonly IMailService _mailService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
        //_mailService = mailService;
    }
    [HttpGet("Users")]
    public IActionResult GetUsers()
    {
        var result = _userService.GetAllUsers();

        return Ok(result);
    }
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Some Properties are not valid");

        UserManagerResponse result = await _userService.RegisterUserAsync(model);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Some Properties are not valid");

        UserManagerResponse result = await _userService.LoginUserAsync(model);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    [HttpPost("Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        var result = await _userService.LogoutUserAsync();
        if(result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}
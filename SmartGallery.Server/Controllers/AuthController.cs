﻿//using Microsoft.AspNetCore.Mvc;
//using SmartGallery.Server.Services;
//using SmartGallery.Server.Services.Contracts;
//using SmartGallery.Shared;

//namespace SmartGallery.Server.Controllers;

//[ApiController]
//[Route("api/[controller]")]

//public class AuthController : ControllerBase
//{
//    private readonly IUserService _userService;

//    //private readonly IMailService _mailService;

//    public AuthController(IUserService userService)
//    {
//        _userService = userService;
//        //_mailService = mailService;
//    }

//    [HttpPost("Register")]
//    public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
//    {
//        if(!ModelState.IsValid)
//            return BadRequest("Some Properties are not valid");

//        UserManagerResponse result = await _userService.RegisterUserAsync(model);

//        if(result.IsSuccess)
//            return Ok(result);

//        return BadRequest(result);
//    }

//    [HttpPost("Login")]
//    public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest("Some Properties are not valid");

//        UserManagerResponse result = await _userService.LoginUserAsync(model);

//        if (result.IsSuccess)
//        {
//<<<<<<< HEAD
//            //await _mailService.SendEmailAsync(model.Email, "New Login", $"<h1>Hey New Login to your Account Noticed</h1> <p>New Login To Your Account At {DateTime.Now}</p>",);
//=======
//            await _mailService.SendEmailAsync(model.Email, "New Login", $"<h1>Hey New Login to your Account Noticed</h1> <p>New Login To Your Account At {DateTime.Now}</p>");
//>>>>>>> 53fe50983de7a3b5f58fbf24f5500a2813ff60da
//            return Ok(result);
//        }

//        return BadRequest(result);
//    }
//}
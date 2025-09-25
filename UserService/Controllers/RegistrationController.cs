using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers;

[ApiController]
[Route("Register")]
public class RegistrationController : ControllerBase
{
  private readonly IUserRegister _userRegister;

  public RegistrationController(IUserRegister userRegister)
  {
    _userRegister = userRegister;
  }


  [HttpPost]
  public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
  {
    var (success, message) = await _userRegister.RegisterUserAsync(user);
    if (!success)
      return BadRequest(message);

    return Ok(new { message });
  }
  
  
};



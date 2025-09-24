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
  public IActionResult Register([FromBody] RegisterUserDto user)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var (success, message) = _userRegister.RegisterUser(user);

    if (!success)
      return BadRequest(message);

    return Ok(new { Message = message });
  }
  
  
};



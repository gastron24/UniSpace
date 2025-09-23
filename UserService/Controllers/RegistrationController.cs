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
  public IActionResult Register([FromBody] RegistrationModelDto model)
  {
    Console.WriteLine($"Приветствую в UniSpace экосистеме!");

    var (succes, message) = _userRegister.RegisterUser(model);

    if (!succes)
      return BadRequest(message);

    return Ok(new
    {
      Message = message,
      User = new
      {
        model.firstName,
        model.lastName,
        model.email,
        model.password

      }
    });
  }
}
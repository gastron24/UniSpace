using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Services;

namespace UserService.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRegister _userRegister;

    public AuthController(IUserRegister userRegister)
    {
        _userRegister = userRegister;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var (success, token) = await _userRegister.LoginAsync(loginRequestDto);

        if (!success)
            return Unauthorized(new { message = "Неверный email или пароль" });

        return Ok(new { token });
            
        
    }
    
}
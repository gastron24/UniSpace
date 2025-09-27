using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.DTO.User;
using UserService.Services.UserService;


namespace UserService.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
    {
        _profileService = profileService;
        _logger = logger;
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserProfileDto>> GetMyProfile()
    {
        var userId = GetUserIdFromClaims();
        var profile = await _profileService.GetProfileAsync(userId);

        if (profile == null)
            return NotFound("Пользователь не найден");

        return Ok(profile);
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserIdFromClaims();
        var success = await _profileService.UpdateProfileAsync( userId, dto);

        if (!success)
            return NotFound("Профиль не найден");

        return Ok(new { message = "Профиль обновлён" });
    }

    [HttpPost("top-up")]
    public async Task<IActionResult> TopUpBalance([FromBody] TopUpBalanceDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = GetUserIdFromClaims();
        var success = await _profileService.TopUpBalanceAsync(userId, dto);
        
        if (!success)
            return NotFound("Профиль не найден");

        return Ok(new { message = $"Баланс пополнен на {dto.Amount:C}" });
    }


    private int GetUserIdFromClaims()
    {
        var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        return int.Parse(userIdClaim?.Value ?? "0");
    }
}
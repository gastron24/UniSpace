using Microsoft.AspNetCore.Mvc;
using UserService.DTO.Admin;
using UserService.Services.Admin;

namespace UserService.Controllers;



[ApiController]
[Route("Api/Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private ILogger<AdminController> _logger;

    public AdminController(IAdminService adminService, ILogger<AdminController> logger)
    {
        _adminService = adminService;
        _logger = logger;
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<UserSummaryDto>>> GetAllUsers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 1;
        if(pageSize > 100) pageSize = 100;
        
        var users = await _adminService.GetAllUsersAsync(page, pageSize);
        return Ok(users);
    }
    
    [HttpPut("users/role")]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _adminService.UpdateUserRoleAsync(dto);
        if (!success)
            return NotFound("Пользователь не найден");

        return Ok(new { message = "Роль успешно обновлена" });
    }

    [HttpPut("users/block")]
    public async Task<IActionResult> BlockUser([FromBody] BlockUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _adminService.BlockedUserAsync(dto);
        if (!success)
            return NotFound("Пользователь не найден");

        return Ok(new {  message = dto.IsBlocked ? "Пользователь заблокирован" : "Пользователь разблокирован" });
    }
    
}
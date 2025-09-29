using Microsoft.EntityFrameworkCore;
using UserService.DTO.Admin;
using UserService.Models;

namespace UserService.Services.Admin;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;
    private readonly ILogger<AdminService> _logger;

    public AdminService(DbContext context, IAdminRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserSummaryDto>> GetAllUsersAsync(int page = 1, int pageSize = 20)
    {
        var users = await _repository.GetAllUsersAsync(page, pageSize);
        return users.Select(u => new UserSummaryDto
        {
            Id = u.Id,
            Email = u.Email,
            Role = u.Role,
            Balance = u.Balance,
            IsBlocked = u.IsBlocked,
            CreatedAt = u.CreatedAt

        }).ToList();
    }

    public async Task<bool> UpdateUserRoleAsync(UpdateUserRoleDto dto)
    {
        var user = await _repository.GetUserByIdAsync(dto.UserId);
        if (user == null) return false;

        user.Role = dto.NewRole;
        var success = await _repository.SaveChangesAsync();
        
        if (success)
        {
            _logger.LogInformation("Админ изменил роль пользователя {UserId} на {NewRole}", dto.UserId, dto.NewRole);
        }
        
        return success;
    }

    public async Task<bool> BlockedUserAsync(BlockUserDto dto)
    {
        var user = await _repository.GetUserByIdAsync(dto.UserId);
        if (user == null) return false;
        
        user.IsBlocked = dto.IsBlocked;
       var success = await _repository.SaveChangesAsync();
       
       if (success)
       {
           _logger.LogInformation("Админ {Action} пользователя {UserId}", 
               dto.IsBlocked ? "заблокировал" : "разблокировал", 
               dto.UserId);
       }

       return success;
    }
}

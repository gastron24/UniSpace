using UserService.DTO.Admin;
using UserService.Models;

namespace UserService.Services.Admin;

public interface IAdminService
{
    Task<List<UserSummaryDto>> GetAllUsersAsync(int page = 1, int pageSize = 20);
    Task<bool> UpdateUserRoleAsync(UpdateUserRoleDto dto);
    Task<bool> BlockedUserAsync(BlockUserDto dto);


}
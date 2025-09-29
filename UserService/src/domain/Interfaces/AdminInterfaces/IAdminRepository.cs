using UserService.Models;

namespace UserService.Services.Admin;

public interface IAdminRepository
{
    Task<List<User>> GetAllUsersAsync(int page = 1, int pageSize = 20);
    Task<User> GetUserByIdAsync(int userId);
    Task<bool> SaveChangesAsync();
}
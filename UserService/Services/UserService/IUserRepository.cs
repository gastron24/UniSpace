using UserService.Models;

namespace UserService.Services.UserService;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int userId);
    Task<bool> SaveChangesAsync();
}
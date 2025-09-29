using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure;
using UserService.Models;

namespace UserService.Services.Admin;

public class AdminRepository : IAdminRepository
{
    private readonly UserDb _context;

    public AdminRepository(UserDb context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetAllUsersAsync(int page = 1, int pageSize = 20)
    {
        return await _context.Users
            .OrderBy(u => u.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync()) > 0;
    }
    
}
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Infrastructure;
public class UserDb : DbContext
{
    public UserDb(DbContextOptions<UserDb> options) : base(options)
    { }
    
    public DbSet<User> Users { get; set; } = null!;
    
    
}
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure;

public class UserDb : DbContext
{
    string ConnectionDb = Environment.GetEnvironmentVariable("CONNECTION_STRING");
}
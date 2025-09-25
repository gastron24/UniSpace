using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.DTO;
using UserService.Infrastructure;
using UserService.Models;

namespace UserService.Services;

public class UserRegisterService : IUserRegister
{
    private readonly UserDb _dbContext;
    private readonly string _jwtKey;

    public UserRegisterService(UserDb dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _jwtKey = configuration["jwtKey"] 
            ?? throw new InvalidOperationException("jwtKey - не задан!");
    }

    public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        if (_dbContext.Users.Any(u => u.Email == registerUserDto.Email))
            return (false, "Пользователь с таким Email-адресом уже существует!");

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password);

        var user = new User
        {
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            Email = registerUserDto.Email,
            PasswordHash = passwordHash,
            Balance = 0.00m
        };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return (true, "Пользователь успешно зарегистрирован!");
    }

    public async Task<(bool Success, string? Token )> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email);

        if (user == null)
            return (false, null);

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequestDto.Password, user.PasswordHash);
        if (!isPasswordValid)
            return (false, null);

        var token = GenerateJwtToken(user);
        return (true, token);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, ($"{user.FirstName} {user.LastName}"))
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "Unispace corp.",
            audience: "Clients",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
}

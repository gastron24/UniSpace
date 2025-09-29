using Microsoft.EntityFrameworkCore;
using UserService.Constants;
using UserService.DTO;
using UserService.Infrastructure;
using UserService.Models;

namespace UserService.Services;

public class UserRegisterService : IUserRegister
{
    private readonly UserDb _dbContext;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenService _jwtTokenService;

    public UserRegisterService(
        UserDb dbContext,
        IPasswordService passwordService,
        IJwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;
        _passwordService = passwordService;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == registerUserDto.Email))
            return (false, "Пользователь с таким Email существует!");

        var passwordHash = _passwordService.HashPassword(registerUserDto.Password);

        var user = new User
        {
            Role = UserRoles.User,
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
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email);

        if (user == null)
            return (false, null);

        var isPasswordValid = _passwordService.VerifyPassword(loginRequestDto.Password, user.PasswordHash);
        if (!isPasswordValid)
            return (false, null);

        var token = _jwtTokenService.GenerateJwtToken(user.Id, user.Email, role: user.Role);
        return (true, token);
    }
} 
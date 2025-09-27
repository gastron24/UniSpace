using Microsoft.AspNetCore.Http.HttpResults;
using UserService.DTO.User;

namespace UserService.Services.UserService;

public class ProfileService : IProfileService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ProfileService> _logger;

    public ProfileService(IUserRepository userRepository, ILogger<ProfileService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserProfileDto> GetProfileAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return null;

        return new UserProfileDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Balance = user.Balance,
            Role = user.Role
        };
    }

    public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileDto dto)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return false;

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        return await _userRepository.SaveChangesAsync();
    }

    public async Task<bool> TopUpBalanceAsync(int userId, TopUpBalanceDto dto)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null) return false;

        user.Balance += dto.Amount;
        var success = await _userRepository.SaveChangesAsync();

        if (success)
        {
            _logger.LogInformation("Пользователь {UserId} пополнил баланс на {Amount}", userId, dto.Amount);
        }

        return success;
    }
}
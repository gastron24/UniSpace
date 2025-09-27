using UserService.DTO.User;

namespace UserService.Services.UserService;

public interface IProfileService
{
    Task<UserProfileDto> GetProfileAsync(int userId);
    Task<bool> UpdateProfileAsync(int userId, UserProfileDto profile);
    Task<bool> TopUpBalanceAsync(int userId, TopUpBalanceDto balanceDtoUp);
}
namespace UserService.Services;

public interface IJwtTokenService
{
    string GenerateJwtToken(int userId, string email, string role);
    
}
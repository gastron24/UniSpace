using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserService.Constants;

namespace UserService.Services;


public class JwtTokenService : IJwtTokenService
{
    private readonly string _secret;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryMinutes;

    public JwtTokenService(IConfiguration config)
    {
        _secret = config["JwtSettings:Secret"]
                  ?? throw new ArgumentNullException("JwtSettings:Secret не задан");
        _issuer = config["JwtSettings:Issuer"] ?? "UniSpace";
        _audience = config["JwtSettings:Audience"] ?? "UniSpaceUsers";
        _expiryMinutes = int.Parse(config["JwtSettings:ExpiryMinutes"] ?? "60");
    }

    public string GenerateJwtToken(int userId, string email, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        var token = new JwtSecurityToken(
        issuer: _issuer,
        audience: _audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
        signingCredentials: creds
            );
      
        return new JwtSecurityTokenHandler().WriteToken(token);
        
    }
}
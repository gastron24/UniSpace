namespace UserService.DTO.User;

public class UserProfileDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsBlocked { get; set; }
    public string Role { get; set; } = string.Empty;
}
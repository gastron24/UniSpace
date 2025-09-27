using System.ComponentModel.DataAnnotations;

namespace UserService.DTO.User;

public class UpdateProfileDto
{
    [Required]
    [StringLength(maximumLength: 30, MinimumLength = 2)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(maximumLength: 30, MinimumLength = 2)]
    public string LastName { get; set; } = string.Empty;
}
using System.ComponentModel.DataAnnotations;

namespace UserService.DTO.Admin;

public class UpdateUserRoleDto
{
    [Required]
    public int UserId { get; set; }
    
    [Required]
    [RegularExpression(@"^(User|Admin)$", ErrorMessage = "Роль должна быть 'User' или 'Admin'" )]
    public string NewRole { get; set; } = string.Empty;
    
    
}
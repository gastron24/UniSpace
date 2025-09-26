using System.ComponentModel.DataAnnotations;

namespace UserService.DTO.Admin;

public class BlockUserDto
{
    [Required]
    public int UserId { get; set; }
    public bool IsBlocked { get; set; } = true;
}
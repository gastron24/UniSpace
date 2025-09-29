using System.ComponentModel.DataAnnotations;

namespace UserService.DTO.User;

public class TopUpBalanceDto
{
    [Required]
    [Range(10, 10000)]
    public decimal Amount { get; set; }
}
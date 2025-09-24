using System.ComponentModel.DataAnnotations;

namespace UserService.DTO;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Имя обязательно!")]
    [MinLength(2, ErrorMessage = "Минимальная длина - 2 символа")]
    [MaxLength(20, ErrorMessage = "Максимальная длина - 20 символов")]
    public string FirstName { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Фамилия обязательна!")]
    [MinLength(2, ErrorMessage = "Минимальная длина фамилии - 2 символа")]
    [MaxLength(20, ErrorMessage = "Максимальная длина фамилии - 20 символов")]
    public string LastName { get; set; } =  String.Empty;
   
    [Required(ErrorMessage = "Почта обязательна!")]
    [MinLength(2, ErrorMessage = "Почта должна быть минимум - 2 символа")]
    [MaxLength(50, ErrorMessage = "Почта не может быть длиннее - 50 символов")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Пароль обязательный!")]
    [MinLength(6, ErrorMessage = "Минимальная длина пароля - 6 символов")]
    [MaxLength(30, ErrorMessage = "Максимальная длина пароля - 30 символов")]
    public string Password { get; set; } = String.Empty;
    
}
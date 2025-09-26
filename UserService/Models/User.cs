using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models;

 public class User
{ 
   
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Role { get; set; } = "User";

    [Required(ErrorMessage = "Имя необходимо для регистрации!")]
    [StringLength(30, MinimumLength = 2, 
        ErrorMessage = "Максимальная длина имени - 30, минимальная - 2")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Фамилия обязательна!")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Максимальная длина фамилии - 30, минимальная - 2")]
    public string LastName { get; set; } = string.Empty;

   [Required(ErrorMessage = "Email необходим для Логина!")]
   [EmailAddress]
   [StringLength(100, ErrorMessage = "Email некорректен!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Пароль необходим для регистрации!")]
    [StringLength(255)]
    public string PasswordHash { get; set; } =  string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; } = 0.00m;
}
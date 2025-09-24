using UserService.DTO;
using UserService.Models;

namespace UserService.Services;

public class UserRegisterService : IUserRegister 
{
    public (bool Success, string Message) RegisterUser(RegisterUserDto user)
    {
        Console.WriteLine($"Регистрация: {user.FirstName} {user.LastName}, \n с почтовым адресом {user.Email}");

        return (true, "Пользователь успешно зарегистрирован");
    }
}
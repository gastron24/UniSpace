using UserService.DTO;
using UserService.Models;

namespace UserService.Services;

public class UserRegister : IUserRegister 
{
    public (bool Success, string Message) RegisterUser(RegistrationModelDto model)
    {
        if (string.IsNullOrWhiteSpace(model.firstName))
            return (false, "Имя обязательно");

        if (model.firstName.Length < 2 || model.firstName.Length > 15)
            return (false, "Имя должно быть от 2 до 15 символов");
        
        if (string.IsNullOrWhiteSpace(model.lastName))
            return (false, "Фамилия обязательна");

        if (model.lastName.Length < 2 || model.lastName.Length > 25)
            return (false, "Фамилия должна быть от 2 до 25 символов");

        if (string.IsNullOrWhiteSpace(model.email))
            return (false, "Email-адрес некорректен");

        if (model.password.Length < 6 && model.password.Length > 20)
            return (false, "Длина пароля должна быть от 6 до 20 символов");
        
        Console.WriteLine($"Зарегистрирован {model.firstName } {model.lastName}, " +
                          $"с почтой {model.email} - Успешно зарегистрирован! ");
        return (true, "Успешная регистрация");
    }
}
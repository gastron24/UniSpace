using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Models;

namespace UserService.Services;

public interface IUserRegister
{
    (bool Success, string Message) RegisterUser(RegistrationModelDto registrationModelDto);
}
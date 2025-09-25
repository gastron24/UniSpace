using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UserService.DTO;
using UserService.Models;

namespace UserService.Services;

public interface IUserRegister
{
    Task<(bool Success, string Message)> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<(bool Success, string? Token)> LoginAsync(LoginRequestDto loginRequestDto); 
}
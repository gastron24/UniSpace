using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace UserService.Models;

public class User
{
    
    public int Id { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }

    public decimal Balance { get; set; } = 0;

    public User()
    {
        Id++;
    }
}
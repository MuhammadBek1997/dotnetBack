using FudballManagement.Domain.Enums;

namespace FudballManagement.Application.DTOs.Commons;
public class LoginWithEmailDto
{
    public UserType UserType { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

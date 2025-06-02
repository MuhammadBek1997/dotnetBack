using FudballManagement.Domain.Enums;

namespace FudballManagement.Application.DTOs.Commons;
public class LoginWithPhoneNumberDto
{
    public UserType UserType { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}

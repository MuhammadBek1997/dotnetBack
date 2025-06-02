using FudballManagement.Application.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FudballManagement.Application.DTOs.Admin;
public class AdminCreateDto
{
    public string FullName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [StrongPassword]
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public IFormFile? ProfilePhoto { get; set; }
    [PhoneNumber]
    public string PhoneNumber { get; set; }

}

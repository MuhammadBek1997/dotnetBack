using FudballManagement.Application.Attributes;
using Microsoft.AspNetCore.Http;
using Npgsql.PostgresTypes;
using System.ComponentModel.DataAnnotations;

namespace FudballManagement.Application.DTOs.Customer;
public class CustomerCreateDto
{
    public string FullName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [StrongPassword]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage ="Password does not match")]
    public string ConfirmPassword { get; set; }
    public IFormFile? ProfilePhoto { get; set; }
    [PhoneNumber]
    public string PhoneNumber { get; set; }
    public int Age {  get; set; }
    public string? Bio {  get; set; }
}

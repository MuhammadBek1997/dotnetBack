using FudballManagement.Application.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FudballManagement.Application.DTOs.Customer;
public class CustomerProfileDto
{
    public long CustomerId { get; set; }
    [Required]
    [MaxLength(45)]
    public string FullName { get; set; }
    [PhoneNumber]
    public string PhoneNumber {  get; set; }

    public string Email {  get; set; }
    public int Age {  get; set; }
    public string? Bio {  get; set; }
    public IFormFile ProfileMedia { get; set; }

}

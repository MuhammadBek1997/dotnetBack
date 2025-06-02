using FudballManagement.Application.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FudballManagement.Application.DTOs.Admin;
public  class AdminProfileDto
{
    public long Id { get; set; }
    [Required]
    [MaxLength(45)]
    public string FullName { get; set; }
    [PhoneNumber]
    public string PhoneNumber { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public IFormFile? ProfileMedia { get; set; }
}

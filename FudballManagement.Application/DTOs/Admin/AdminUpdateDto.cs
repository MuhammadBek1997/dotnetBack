using FudballManagement.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FudballManagement.Application.DTOs.Admin;
public class AdminUpdateDto
{
    public long Id { get; set; }
    public string FullName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [PhoneNumber]
    public string PhoneNumber { get; set; }
}

using FudballManagement.Application.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FudballManagement.Application.DTOs.Admin;
public class AdminProfileResponseDto
{
    public long Id { get; set; }
    [Required]
    [MaxLength(45)]
    public string FullName { get; set; }
    [PhoneNumber]
    public string PhoneNumber { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string? PhotoPath{ get; set; }
    [JsonIgnore]
    public DateTime UpdateAtUtc{ get; set; }
    public DateTime UpdateAt => UpdateAtUtc.AddHours(5);
}

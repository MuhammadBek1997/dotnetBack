using System.Text.Json.Serialization;

namespace FudballManagement.Application.DTOs.Admin;
public class AdminResponseDto
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? PhotoPath { get; set; }
    [JsonIgnore]
    public DateTime CreatAtUtc { get; set; }
    public DateTime CreatedAt => CreatAtUtc.AddHours(5);
}

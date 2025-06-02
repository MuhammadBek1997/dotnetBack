using System.Text.Json.Serialization;

namespace FudballManagement.Application.DTOs.Customer;
public class CustomerProfileResponseDto
{
    public long Id {  get; set; }
    public string PhotoPath {  get; set; }
    public int Age { get; set; }
    public string Bio { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    [JsonIgnore]
    public DateTime UpdateAtUtc { get; set; }
    public DateTime UpdateAt => UpdateAtUtc.AddHours(5);
}

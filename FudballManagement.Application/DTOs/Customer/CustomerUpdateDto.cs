using FudballManagement.Application.Attributes;

namespace FudballManagement.Application.DTOs.Customer;
public class CustomerUpdateDto
{
    public long Id {  get; set; }
    public string FullName { get; set; }
    [PhoneNumber]
    public string PhoneNumber { get; set; }

    public string Email { get; set; }
    public int Age { get; set; }
    public string? Bio { get; set; }
}

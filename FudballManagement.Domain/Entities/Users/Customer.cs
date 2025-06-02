using FudballManagement.Domain.Commons;
using FudballManagement.Domain.Entities.Stadiums;

namespace FudballManagement.Domain.Entities.Users;
public class Customer : Auditable
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? Bio {  get; set; }
    public int Age {  get; set; }
    public ICollection<StadiumRating> StadiumRatings { get; set; }
    public ICollection<StadiumComment> stadiumComments { get; set; }
}

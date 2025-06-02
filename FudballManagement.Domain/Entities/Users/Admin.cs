using FudballManagement.Domain.Commons;
using FudballManagement.Domain.Entities.Stadiums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudballManagement.Domain.Entities.Users;
[Table("Admins")]
public  class Admin : Auditable
{
    public string FullName {  get; set; }
    public string PhoneNumber {  get; set; }
    public string Email {  get; set; }
    public string Password { get; set; }
    public string? ProfilePhoto { get; set; }
    public ICollection<Stadium> Stadiums { get; set; }
}

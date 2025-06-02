using FudballManagement.Domain.Commons;
using FudballManagement.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FudballManagement.Domain.Entities.Stadiums;
public class StadiumRating : Auditable
{
    public long StadiumId { get; set; }
    [JsonIgnore]
    public virtual Stadium Stadium { get; set; }
    public long CustomerId { get; set; }
    [JsonIgnore]
    public virtual Customer Customer { get; set; }
    [Range(1, 5)]
    public double Rating {  get; set; }
}

using FudballManagement.Domain.Commons;
using System.Text.Json.Serialization;

namespace FudballManagement.Domain.Entities.Stadiums;
public class StadiumMedia : Auditable
{
    public long StadiumId { get; set; }
    [JsonIgnore]
    public virtual Stadium Stadium { get; set; }
    public string PhotoUrl { get; set; }

}

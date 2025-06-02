using FudballManagement.Domain.Commons;
using FudballManagement.Domain.Entities.Users;
using System.Text.Json.Serialization;

namespace FudballManagement.Domain.Entities.Stadiums;
public  class StadiumComment : Auditable
{
    public long StadiumId { get; set; }
    [JsonIgnore]
    public virtual Stadium Stadium { get; set; }
    public long CustomerId { get; set; }
    [JsonIgnore]
    public virtual Customer Customer { get; set; }
    public string TextMessage { get; set; } 
    public bool IsAnonymous { get; set; }
}

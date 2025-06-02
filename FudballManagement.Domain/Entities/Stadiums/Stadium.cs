using FudballManagement.Domain.Commons;
using FudballManagement.Domain.Entities.Users;
using System.Text.Json.Serialization;

namespace FudballManagement.Domain.Entities.Stadiums;
public class Stadium : Auditable
{
    public string StadiumName { get; set; }
    public string StadiumPhoneNumber { get; set; }
    public string LandMark { get; set; }
    public bool HasLights { get; set; }
    public decimal PricePerHour { get; set; }
    public string Rules { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<StadiumComment> StadiumComments { get; set; } = new List<StadiumComment>();
    public ICollection<StadiumMedia> StadiumMedias { get; set; } = new List<StadiumMedia>();
    public ICollection<StadiumRating> stadiumRatings { get; set; } = new List<StadiumRating>();
    public long AdminId {  get; set; }
    [JsonIgnore]
    public Admin Admin { get; set; }
}

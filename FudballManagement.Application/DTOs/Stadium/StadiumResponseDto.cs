using FudballManagement.Application.DTOs.Stadium.SadiumComment;
using FudballManagement.Domain.Entities.Stadiums;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace FudballManagement.Application.DTOs.Stadium;
public class StadiumResponseDto
{
    public long Id { get; set; }
    public string StadiumName { get; set; }
    public string StadiumPhoneNumber { get; set; }
    public string LandMark { get; set; }
    public bool HasLights { get; set; }
    public decimal PricePerHour { get; set; }
    public string Rules { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedAt { get; set; }
    public double AverageRating { get; set; }
    public int TotalVotes { get; set; }
    public ICollection<StadiumMedia> StadiumMedias { get; set; }
    public List<StadiumComentCreateDto> Comments { get; set; }
}

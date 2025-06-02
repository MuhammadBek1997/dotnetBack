using Microsoft.AspNetCore.Http;

namespace FudballManagement.Application.DTOs.Stadium;
public  class StadiumCreateDto
{
    public string StadiumName { get; set; }
    public string StadiumPhoneNumber { get; set; }
    public string LandMark { get; set; }
    public bool HasLights { get; set; }
    public decimal PricePerHour { get; set; }
    public string Rules { get; set; }
    public ICollection<IFormFile>? StadiumMedias { get; set; }
    public long AdminId {  get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

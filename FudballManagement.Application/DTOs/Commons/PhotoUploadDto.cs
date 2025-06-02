using Microsoft.AspNetCore.Http;

namespace FudballManagement.Application.DTOs.Commons;
public class PhotoUploadDto
{
    public IFormFile ProfileMedia { get; set; }
    public string FileName {  get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set;} = DateTime.Now;
}
using System.Globalization;

namespace FudballManagement.Application.DTOs.Stadium;
public class ChangeStadiumNameDto
{
    public long StadiumId { get; set; }
    public string NewStadiumName { get; set; }
}


namespace FudballManagement.Application.DTOs.Order;
public  class OrderResponseDto
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public string CustomerFullName { get; set; }
    public string StadiumPhoneNumber { get; set; }
    public long StadiumId { get; set; }
    public string StadiumName { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public decimal OverallPrice { get; set; }
}

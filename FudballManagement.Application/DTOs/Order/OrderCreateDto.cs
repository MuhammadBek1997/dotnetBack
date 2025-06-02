namespace FudballManagement.Application.DTOs.Order;
public  class OrderCreateDto
{
    public long CustomerId {  get; set; }
    public long StadiumId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}

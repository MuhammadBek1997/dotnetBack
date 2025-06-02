using FudballManagement.Domain.Commons;
using FudballManagement.Domain.Entities.Stadiums;
using FudballManagement.Domain.Entities.Users;

namespace FudballManagement.Domain.Entities.Order;
public class Orders : Auditable
{
    public long CustomerId {  get; set; }
    public Customer Customer { get; set; }
    public long StadiumId {  get; set; }
    public Stadium Stadiums { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal OverallPrice { get; set;  }
}

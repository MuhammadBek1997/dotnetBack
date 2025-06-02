namespace FudballManagement.Domain.Commons;
public  class Auditable : Base
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get;set; }
}

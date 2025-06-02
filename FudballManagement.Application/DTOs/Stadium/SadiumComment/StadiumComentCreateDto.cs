namespace FudballManagement.Application.DTOs.Stadium.SadiumComment;

public class StadiumComentCreateDto
{
    public long StadiumId { get; set; }
    public long CustomerId { get; set; }
    public string TextMessage { get; set; }
    public bool IsAnonymous { get; set; }
}

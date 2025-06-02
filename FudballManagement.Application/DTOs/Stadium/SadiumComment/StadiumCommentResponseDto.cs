namespace FudballManagement.Application.DTOs.Stadium.SadiumComment;
public class StadiumCommentResponseDto
{
    public long Id { get; set; }
    public long StadiumId { get; set; }
    public string TextMessage { get; set; }
    public bool IsAnonymous { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
}

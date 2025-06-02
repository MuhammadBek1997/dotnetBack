using FudballManagement.Application.DTOs.Stadium.SadiumComment;

namespace FudballManagement.Application.Services.Interfaces;
public interface ICommentService
{
    Task<StadiumCommentResponseDto> CreateAsync(StadiumComentCreateDto dto, CancellationToken token);
    Task<bool> UpdateAsync(long id, StadiumCommentUpdateDto dto, CancellationToken token);
    Task<bool> DeleteAsync(long id, CancellationToken token);
    Task<IEnumerable<StadiumCommentResponseDto>> GetByStadiumIdAsync(long stadiumId);
}

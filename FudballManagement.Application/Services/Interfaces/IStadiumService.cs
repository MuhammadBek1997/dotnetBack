using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.DTOs.Stadium;
using FudballManagement.Domain.Entities.Stadiums;
using FudballManagement.Domain.Entities.Users;
using System.Linq.Expressions;

namespace FudballManagement.Application.Services.Interfaces;
public interface IStadiumService
{
    public Task<StadiumResponseDto> CreateAsync(StadiumCreateDto stadiumCreate, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(StadiumUpdateDto stadiumUpdate, CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(long StadiumId, CancellationToken cancellationToken);
    public Task<StadiumResponseDto> GetAsync(Expression<Func<Stadium, bool>> expression);
    public Task<IEnumerable<StadiumResponseDto>> GetAllAsync();

    public Task<bool> ChangeStadiumName(ChangeStadiumNameDto changeStadiumName, CancellationToken cancellationToken);
}

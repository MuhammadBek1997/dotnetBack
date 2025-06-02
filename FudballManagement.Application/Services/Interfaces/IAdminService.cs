using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Domain.Entities.Users;
using System.Linq.Expressions;

namespace FudballManagement.Application.Services.Interfaces;
public interface IAdminService
{
    public Task<AdminResponseDto> CreateAsync(AdminCreateDto adminCreateDto,CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto,CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(long AdminId,CancellationToken cancellationToken);
    public Task<AdminResponseDto> GetAsync(Expression<Func<Admin, bool>> expression);
    public Task<IEnumerable<AdminResponseDto>> GetAllAsync();

    public Task<bool> ChangePassword(ChangePasswordDto changePasswordDto,CancellationToken cancellationToken);

    public Task<AdminProfileResponseDto> GetProfileAsync(long AdminId,CancellationToken cancellationToken);
    public Task<AdminProfileResponseDto> UpdateProfileAsync(AdminProfileDto profileDto,CancellationToken cancellationToken);
}

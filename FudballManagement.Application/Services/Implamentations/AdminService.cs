using AutoMapper;
using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Extensions;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Users;
using FudballManagement.Domain.Enums;
using FudballManagement.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Channels;

namespace FudballManagement.Application.Services.Implamentations;
public class AdminService : IAdminService
{
    private readonly IGenericRepository<Admin> _repositories;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    public AdminService(IGenericRepository<Admin> repositories, IMapper mapper,IFileService fileService)
    {
        _repositories = repositories;
        this._mapper = mapper;
        this._fileService = fileService;
    }

    public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
    {
        var HasAdmin = await _repositories.GetAsync(c => c.PhoneNumber == changePasswordDto.PhoneNumber);
        if (HasAdmin is null)
            throw new CustomException(404, "Admin not found");

        var CheckPassword = changePasswordDto.OldPassword.Encrypt();
        if (HasAdmin.Password != CheckPassword || changePasswordDto.NewPassword == changePasswordDto.OldPassword)
            throw new CustomException(400, "Password error");

        HasAdmin.Password = changePasswordDto.NewPassword.Encrypt();
        HasAdmin.UpdatedAt = DateTime.UtcNow;
         var changed = await _repositories.UpdateAsync(HasAdmin, cancellationToken);
        if (changed is null)
            throw new CustomException(500, "Internal Server Error");
        return true;
    }

    public async Task<AdminResponseDto> CreateAsync(AdminCreateDto adminCreateDto, CancellationToken cancellationToken)
    {
        var HasAdmin = await _repositories.GetAsync(a => a.PhoneNumber == adminCreateDto.PhoneNumber);

        if (HasAdmin is not null)
            throw new CustomException(404, "Admin Already exist");
        if (adminCreateDto.Password != adminCreateDto.ConfirmPassword)
            throw new CustomException(400, "Password did not match");

        adminCreateDto.Password = adminCreateDto.Password.Encrypt();
        var Mapping = _mapper.Map<Admin>(adminCreateDto);

        Mapping.CreatedAt = DateTime.UtcNow;



        if (adminCreateDto.ProfilePhoto is not null)
        {
            var PhotoPath = await _fileService.SaveProfilePhotoAsync(
                new PhotoUploadDto
                {
                    ProfileMedia = adminCreateDto.ProfilePhoto,
                    FileName = adminCreateDto.ProfilePhoto.FileName
                },
                UserType.Admins
                );

            Mapping.ProfilePhoto = PhotoPath;
        }

        var result = await _repositories.CreateAsync(Mapping, cancellationToken);
        if (result is null)
            throw new CustomException(500, "Internal Server Error");

        return _mapper.Map<AdminResponseDto>(result);
         
    }

    public async Task<bool> DeleteAsync(long AdminId, CancellationToken cancellationToken)
    {
        var HasAdmin = await _repositories.GetAsync(a => a.Id == AdminId);
        if (HasAdmin is null)
            throw new CustomException(404, "Admin Not found");

        await _repositories.DeleteAsync(a => a.Id == AdminId, cancellationToken);
        return true;
    }

    public async Task<IEnumerable<AdminResponseDto>> GetAllAsync()
    {
        var Admins = await _repositories.GetAllAsync();
        var mappingAdmins = _mapper.Map<List<AdminResponseDto>>(Admins);

        return mappingAdmins;
    }

    public async Task<AdminResponseDto> GetAsync(Expression<Func<Admin, bool>> expression)
    {
        var Admin = await _repositories.GetAsync(expression);
        var MappingAdmin = _mapper.Map<AdminResponseDto>(Admin);
        return MappingAdmin;
    }

    public async Task<AdminProfileResponseDto> GetProfileAsync(long AdminId, CancellationToken cancellationToken)
    {
        var FindAdmin = await _repositories.GetAsync(g => g.Id == AdminId);
        if (FindAdmin is null)
            throw new CustomException(404, " Admin profile not found");

        return _mapper.Map<AdminProfileResponseDto>(FindAdmin);
    }

    public async Task<bool> UpdateAsync(AdminUpdateDto adminUpdateDto, CancellationToken cancellationToken)
    {
        var admin = await _repositories.GetAsync(a => a.Id == adminUpdateDto.Id);
        if (admin is null)
            throw new CustomException(404, "Admin not found");

        var mappingAdmin = _mapper.Map(adminUpdateDto, admin);
        mappingAdmin.UpdatedAt = DateTime.UtcNow;
        await _repositories.UpdateAsync(admin, cancellationToken);
        return true;
    }

    public async Task<AdminProfileResponseDto> UpdateProfileAsync( AdminProfileDto profileDto, CancellationToken cancellationToken)
    {
        var Admin = await _repositories.GetAsync(a => a.Id == profileDto.Id);
        if (Admin == null) throw new CustomException(404, "Admin not found");

        Admin.UpdatedAt = DateTime.UtcNow;
        _mapper.Map(profileDto, Admin);
        var updated = await _repositories.UpdateAsync(Admin,cancellationToken);
        if (updated is null)
            throw new CustomException(500, "Internal Server Error");
        return _mapper.Map<AdminProfileResponseDto>(updated);
    }
}

using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Domain.Enums;

namespace FudballManagement.Application.Services.Interfaces;
public  interface IFileService
{
    Task<string> SaveProfilePhotoAsync(PhotoUploadDto photoUploadDto, UserType userType, string oldPhotoPath = null);
    Task<bool> DeleteProfilePhotoAsync(string PhotoPath, UserType userType);

}

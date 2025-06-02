using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace FudballManagement.Application.Services.Implamentations;
public class FileService : IFileService
{
    private readonly string _rootDirectory;

    public FileService(IConfiguration configuration)
    {
        _rootDirectory = "wwwroot"; // Use the actual base directory as per your structure
        if (!Directory.Exists(_rootDirectory))
            Directory.CreateDirectory(_rootDirectory);
    }

    public Task<bool> DeleteProfilePhotoAsync(string photoPath, UserType userType)
    {
        try
        {
            string fileName = Path.GetFileName(photoPath);
            string fullPath = Path.Combine(_rootDirectory, userType.ToString(), fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public async Task<string> SaveProfilePhotoAsync(PhotoUploadDto photoUploadDto, UserType userType, string oldPhotoPath = null)
    {
        string targetDirectory = Path.Combine(_rootDirectory, userType.ToString());

        if (!Directory.Exists(targetDirectory))
            Directory.CreateDirectory(targetDirectory);

        if (!string.IsNullOrEmpty(oldPhotoPath))
            await DeleteProfilePhotoAsync(oldPhotoPath, userType);

        string extension = Path.GetExtension(photoUploadDto.FileName);
        string fileName = $"{Guid.NewGuid()}{extension}";
        string filePath = Path.Combine(targetDirectory, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await photoUploadDto.ProfileMedia.CopyToAsync(fileStream);
        }

        // Return a path relative to the root (for front-end use)
        return $"/{userType}/{fileName}";
    }
}

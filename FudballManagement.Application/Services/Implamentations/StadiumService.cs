using AutoMapper;
using AutoMapper.QueryableExtensions;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.DTOs.Stadium;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Stadiums;
using FudballManagement.Domain.Enums;
using FudballManagement.Infrastructure.DbContexts;
using FudballManagement.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Channels;
using FudballManagement.Application.Extensions;

namespace FudballManagement.Application.Services.Implamentations;
public class StadiumService : IStadiumService
{
    private readonly IGenericRepository<Stadium> _repository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public StadiumService(IGenericRepository<Stadium> repository, IFileService fileService, IMapper mapper, AppDbContext dbContext)
    {
        _repository = repository;
        _fileService = fileService;
        _mapper = mapper;
        _appDbContext = dbContext;
        
    }

    public async Task<bool> ChangeStadiumName(ChangeStadiumNameDto changeStadiumName, CancellationToken cancellationToken)
    {
        var Stadium = await _repository.GetAsync(c => c.Id == changeStadiumName.StadiumId);
        if (Stadium is null)
            throw new CustomException(404, "Stadium not found");
        Stadium.StadiumName = changeStadiumName.NewStadiumName;
        var Updated = await  _repository.UpdateAsync(Stadium, cancellationToken);
        Stadium.UpdatedAt = DateTime.UtcNow; 
        return true;
    }

    public async Task<StadiumResponseDto> CreateAsync(StadiumCreateDto stadiumCreate, CancellationToken cancellationToken)
    {
        var existStadium = await _repository.GetAsync(a => a.StadiumName == stadiumCreate.StadiumName);
        if (existStadium is not null)
            throw new CustomException(409, "Stadium already exists");

        
        var stadium = _mapper.Map<Stadium>(stadiumCreate);
        var now = DateTimeHelper.GetUtcPlus5Now();

       
        if (stadiumCreate.StadiumMedias is not null && stadiumCreate.StadiumMedias.Any())
        {
            stadium.StadiumMedias = new List<StadiumMedia>();

            foreach (var formFile in stadiumCreate.StadiumMedias)
            {
                var photoDto = new PhotoUploadDto
                {
                    FileName = formFile.FileName,
                    ProfileMedia = formFile,
                    CreatedAt = stadium.CreatedAt,
                    
                };

                string photoPath = await _fileService.SaveProfilePhotoAsync(photoDto, UserType.Stadium);

                stadium.StadiumMedias.Add(new StadiumMedia
                {
                    PhotoUrl = photoPath,
                    CreatedAt = now,
                    UpdatedAt = now
                });
            }
        } ;

        // Set creation timestamps for Stadium itself
        stadium.CreatedAt = now;
        stadium.UpdatedAt = now;


        var result = await _repository.CreateAsync(stadium, cancellationToken);
        return _mapper.Map<StadiumResponseDto>(result);
    }


    public async Task<bool> DeleteAsync(long StadiumId, CancellationToken cancellationToken)
    {
        var stadium = await _repository.GetAllAsync(s => s.Id == StadiumId)
            ?? throw new CustomException(404, "Stadium not found");

        await _repository.DeleteAsync(s => s.Id == StadiumId,cancellationToken);
        return true;

    }

    public async Task<IEnumerable<StadiumResponseDto>> GetAllAsync()
    {
        //var stadium = await _repository.GetAllAsync();
        //var stadiums = await stadium.ToListAsync();

        var stadiumQuery = await _repository.GetAllAsync();

        var stadium3 = await stadiumQuery
            .ProjectTo<StadiumResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StadiumResponseDto>>(stadium3);
    }

    public async Task<StadiumResponseDto> GetAsync(Expression<Func<Stadium, bool>> expression)
    {
        var stadiumQuery = await _repository.GetAllAsync();

        var stadiumDto = await stadiumQuery
            .Where(expression)
            .ProjectTo<StadiumResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (stadiumDto == null)
            throw new CustomException(404, "Stadium not found");

        return stadiumDto;
    }


    public async Task<bool> UpdateAsync(StadiumUpdateDto stadiumUpdate, CancellationToken cancellationToken)
    {
        var exist = await _repository.GetAsync(e => e.Id == stadiumUpdate.Id, includes: new[] { "StadiumMedias" });

        if (exist == null)
            throw new CustomException(404, "Stadium not found");

        // Update basic fields
        _mapper.Map(stadiumUpdate, exist);

        // Handle media
        if (stadiumUpdate.StadiumMedias is not null && stadiumUpdate.StadiumMedias.Any())
        {
            // Delete old media files from storage
            foreach (var media in exist.StadiumMedias.ToList())
            {
                await _fileService.DeleteProfilePhotoAsync(media.PhotoUrl, UserType.Stadium);
                _appDbContext.stadiumMedias.Remove(media); // Remove from DB explicitly
            }

            // Add new media
            var now = DateTimeHelper.GetUtcPlus5Now();
            exist.StadiumMedias = new List<StadiumMedia>();

            foreach (var formFile in stadiumUpdate.StadiumMedias)
            {
                var photoDto = new PhotoUploadDto
                {
                    FileName = formFile.FileName,
                    ProfileMedia = formFile
                };

                var photoPath = await _fileService.SaveProfilePhotoAsync(photoDto, UserType.Stadium);

                exist.StadiumMedias.Add(new StadiumMedia
                {
                    PhotoUrl = photoPath,
                    CreatedAt = now,
                    UpdatedAt = now
                });
            }
        }

        var result = await _repository.UpdateAsync(exist, cancellationToken);

        if (result is null)
            throw new CustomException(500, "Failed to update stadium");

        return true;
    }


}

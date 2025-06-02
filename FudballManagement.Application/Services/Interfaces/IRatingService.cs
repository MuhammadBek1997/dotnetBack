using FudballManagement.Application.DTOs.Stadium;
using FudballManagement.Domain.Entities.Stadiums;

namespace FudballManagement.Application.Services.Interfaces;
public  interface IRatingService
{
    Task<bool> RateStadiumAsync(StadiumRatingDto stadiumRating, CancellationToken cancellationToken);
    Task<double> GetAverageRatingAsync(long stadiumId, CancellationToken cancellationToken);
}

using FudballManagement.Application.DTOs.Stadium;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Stadiums;
using FudballManagement.Domain.Entities.Users;
using FudballManagement.Infrastructure.Repositories.Interfaces;

namespace FudballManagement.Application.Services.Implamentations;
public class RatingService : IRatingService
{
    private readonly IGenericRepository<StadiumRating> _ratingRepo;
    private readonly IGenericRepository<Stadium> _stadiumRepo;
    public RatingService(
        IGenericRepository<StadiumRating> ratingRepo,
        IGenericRepository<Stadium> stadiumRepo)
    {
        _ratingRepo = ratingRepo;
        _stadiumRepo = stadiumRepo;
    }
    public async Task<double> GetAverageRatingAsync(long stadiumId, CancellationToken cancellationToken)
    {
        var stadium = await _stadiumRepo.GetAsync(s => s.Id == stadiumId);

        if (stadium == null)
            return 0;

        var ratings = await _ratingRepo.GetAllAsync(r => r.StadiumId == stadiumId);

        if (!ratings.Any())
            return 0;

        return ratings.Average(r => r.Rating);
    }

    public async Task<bool> RateStadiumAsync(StadiumRatingDto stadiumRating, CancellationToken cancellationToken)
    {
        var existingRating = await _ratingRepo.GetAsync(
            r => r.StadiumId == stadiumRating.StadiumId && r.CustomerId == stadiumRating.CustomerId);

        if (existingRating != null)
        {
            // Update existing rating
            existingRating.Rating = stadiumRating.Rating;
            existingRating.UpdatedAt = DateTime.UtcNow;

            var updated = await _ratingRepo.UpdateAsync(existingRating, cancellationToken);
            return updated != null;
        }
        else
        {
            // Create new rating
            var newRating = new StadiumRating
            {
                StadiumId = stadiumRating.StadiumId,
                CustomerId = stadiumRating.CustomerId,
                Rating = stadiumRating.Rating,
                CreatedAt = DateTime.UtcNow
            };

            var added = await _ratingRepo.CreateAsync(newRating, cancellationToken);
            return added != null;
        }
    }
}

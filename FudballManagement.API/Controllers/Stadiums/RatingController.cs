using FudballManagement.Application.DTOs.Stadium;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Stadiums;
using Microsoft.AspNetCore.Mvc;

namespace FudballManagement.API.Controllers.Stadiums;

[Route("api/[controller]/[action]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    // GET: api/StadiumRatings/average/5
    [HttpGet("average")]
    public async Task<ActionResult> GetAverageRating(long stadiumId, CancellationToken cancellation)
    {
        var average = await _ratingService.GetAverageRatingAsync(stadiumId, cancellation);
        return Ok(new { StadiumId = stadiumId, AverageRating = average });
    }

    // POST: api/StadiumRatings
    [HttpPost]
    public async Task<ActionResult> RateStadium([FromBody] StadiumRatingDto stadiumRating, CancellationToken cancellationToken)
    {
        var success = await _ratingService.RateStadiumAsync(stadiumRating, cancellationToken);
        if (!success)
            return BadRequest("Failed to rate stadium.");

        return Ok("Rating submitted successfully.");
    }
}

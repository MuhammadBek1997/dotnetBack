using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.DTOs.Stadium;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Services.Implamentations;
using FudballManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FudballManagement.API.Controllers.Stadiums;

[Route("api/[controller]/[action]")]
[ApiController]
public class StadiumController : ControllerBase
{
    private readonly IStadiumService _stadiumService;

    public StadiumController(IStadiumService stadiumService)
    {
        _stadiumService = stadiumService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateStadium([FromForm] StadiumCreateDto stadiumCreateDto, CancellationToken token)
    {
        try
        {
            return Ok(await _stadiumService.CreateAsync(stadiumCreateDto, token));
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Server error" + ex.Message);
        }
    }

    [HttpGet]
    [Authorize]

    public async Task<ActionResult<StadiumResponseDto>> GetById(long id)
    {
        try
        {
            return Ok(await _stadiumService.GetAsync(a => a.Id == id));
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error Server" + ex.Message);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<StadiumResponseDto>>> GetAll()
    {
        try
        {
            return Ok(await _stadiumService.GetAllAsync());
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error Server " + ex.Message);
        }
    }

    [HttpPut]
    [Authorize]

    public async Task<ActionResult<bool>> UpdateStadium([FromForm] StadiumUpdateDto stadiumUpdate, CancellationToken token)
    {
        try
        {
            return Ok(await _stadiumService.UpdateAsync(stadiumUpdate, token));
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error Server" + ex.Message);
        }
    }

    [HttpDelete]
    [Authorize]

    public async Task<ActionResult<bool>> DeleteStadium([FromForm] long Id, CancellationToken token)
    {
        try
        {
            return Ok(await _stadiumService.DeleteAsync(Id, token));
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error Server" + ex.Message);
        }
    }

    [HttpPut]
    [Authorize]

    public async Task<ActionResult<bool>> ChangeStadiumName([FromForm] ChangeStadiumNameDto changeStadiumName, CancellationToken token)
    {
        try
        {
            return Ok(await _stadiumService.ChangeStadiumName(changeStadiumName, token));
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Server Error " + ex.Message);
        }
    }
}

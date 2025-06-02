using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FudballManagement.API.Controllers.Users;
[Route("api/[controller]/[action]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAdmin([FromForm] AdminCreateDto adminCreateDto,CancellationToken token)
    {
        try
        {
            return Ok(await _adminService.CreateAsync(adminCreateDto, token));
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Server error" + ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<AdminResponseDto>> GetById(long id)
    {
        try
        {
            return Ok(await _adminService.GetAsync(a => a.Id == id));
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, "Error Server" + ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdminResponseDto>>> GetAll()
    {
        try
        {
            return Ok(await _adminService.GetAllAsync());
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, "Error Server " + ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateAdminAsync([FromForm] AdminUpdateDto adminUpdateDto, CancellationToken token)
    {
        try
        {
            return Ok(await _adminService.UpdateAsync(adminUpdateDto, token));
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, "Error Server" + ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteAdminAsync([FromForm] long Id, CancellationToken token)
    {
        try
        {
            return Ok(await _adminService.DeleteAsync(Id, token));
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, "Error Server" + ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> ChangePasswordAsync([FromForm] ChangePasswordDto changePassword, CancellationToken token)
    {
        try 
        {
            return Ok(await _adminService.ChangePassword(changePassword, token));       
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, "Server Error " + ex.Message);
        }
    }

    [HttpGet("{adminId}")]
    public async Task<ActionResult> GetProfileAsync(long adminId, CancellationToken token)
    {
        try
        {
            return Ok(await _adminService.GetProfileAsync(adminId,token));
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, "Error Server" + ex.Message);
        }
    }

    [HttpPut("{adminId}")]
    public async Task<ActionResult> UpdateProfileAsync([FromForm] AdminProfileDto adminProfileDto, CancellationToken token)
    {
        try
        {
            return Ok(await _adminService.UpdateProfileAsync(adminProfileDto, token));
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, "Error Server" + ex.Message);
        }
    }
}

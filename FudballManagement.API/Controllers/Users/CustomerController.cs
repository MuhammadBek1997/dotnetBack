using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.DTOs.Customer;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Services.Implamentations;
using FudballManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FudballManagement.API.Controllers.Users;
[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomer([FromForm] CustomerCreateDto customerCreate, CancellationToken token)
    {
        try
        {
            return Ok(await _customerService.CreateAsync(customerCreate, token));
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
    public async Task<ActionResult<CustomerResponseDto>> GetById(long id)
    {
        try
        {
            return Ok(await _customerService.GetAsync(a => a.Id == id));
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
    public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
    {
        try
        {
            return Ok(await _customerService.GetAllAsync());
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
    public async Task<ActionResult<bool>> UpdateCustomerAsync([FromForm] CustomerUpdateDto customerUpdate, CancellationToken token)
    {
        try
        {
            return Ok(await _customerService.UpdateAsync(customerUpdate, token));
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
    public async Task<ActionResult<bool>> DeleteCustomerAsync([FromForm] long Id, CancellationToken token)
    {
        try
        {
            return Ok(await _customerService.DeleteAsync(Id, token));
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
    public async Task<ActionResult<bool>> ChangePasswordAsync([FromForm] ChangePasswordDto changePassword, CancellationToken token)
    {
        try
        {
            return Ok(await _customerService.ChangePassword(changePassword, token));
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

    [HttpGet("{CustomerId}")]
    public async Task<ActionResult> GetProfileAsync(long CustomerId, CancellationToken token)
    {
        try
        {
            return Ok(await _customerService.GetProfileAsync(CustomerId, token));
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

    [HttpPut("{CustomerId}")]
    public async Task<ActionResult> UpdateProfileAsync([FromForm] CustomerProfileDto customerProfile, CancellationToken token)
    {
        try
        {
            return Ok(await _customerService.UpdateProfileAsync(customerProfile, token));
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
}

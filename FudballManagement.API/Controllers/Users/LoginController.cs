using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FudballManagement.API.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginWithEmailDto loginWithEmailDto)
        {
            try
            {
                return Ok(await _tokenService.GenerateToken(loginWithEmailDto));
            }
            catch(CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using FudballManagement.Application.DTOs.Stadium.SadiumComment;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Services.Implamentations;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Stadiums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FudballManagement.API.Controllers.Stadiums;

[Route("api/[controller]/[action]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        this._commentService = commentService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] StadiumComentCreateDto dto, CancellationToken token)
    {
        try
        {
            var result = await _commentService.CreateAsync(dto, token);
            return Ok(result);
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error in server " + ex.Message);
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(long id, [FromBody] StadiumCommentUpdateDto dto, CancellationToken token)
    {
        try
        {
            var result = await _commentService.UpdateAsync(id, dto, token);
            return Ok(result);
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Serverda xatolik yuz berdi: " + ex.Message);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id, CancellationToken token)
    {
        try
        {
            var result = await _commentService.DeleteAsync(id, token);
            return Ok(result);
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Serverda xatolik yuz berdi: " + ex.Message);
        }
    }
    [Authorize]
    [HttpGet()]
    public async Task<ActionResult> GetStadiumComent(long StadiumId)
    {
        try
        {
            var result = await _commentService.GetByStadiumIdAsync(StadiumId);
            return Ok(result);
        }
        catch (CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Serverda xatolik yuz berdi: " + ex.Message);
        }
    }
}

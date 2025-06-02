using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Domain.Enums;

namespace FudballManagement.Application.Services.Interfaces;
public interface ITokenService
{
    public Task<string> GenerateToken(LoginWithEmailDto loginWithEmail);
}

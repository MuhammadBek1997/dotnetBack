using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Users;
using FudballManagement.Domain.Enums;
using FudballManagement.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FudballManagement.Application.Services.Implamentations;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IGenericRepository<Admin> _Adminrep;
    private readonly IGenericRepository<Customer> _CustomerRep;
    private readonly string _secretKey;  // Fixed typo from "secterKey"
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryMinutes;

    public TokenService(IConfiguration configuration, IGenericRepository<Admin> adminrep, IGenericRepository<Customer> customerRep)
    {
        _configuration = configuration;
        _Adminrep = adminrep;
        _CustomerRep = customerRep;

        // Fixed syntax (removed asterisks)
        _secretKey = configuration["Jwt:SecretKey"];
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
        _expiryMinutes = int.Parse(configuration["Jwt:ExpiryMinutes"] ?? "60");
    }

    public async Task<string> GenerateToken(LoginWithEmailDto loginWithEmail)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new();

        if (loginWithEmail.UserType == UserType.Admins)
        {
            var admin = await _Adminrep.GetAsync(a => a.Email == loginWithEmail.Email);
/*
            if(!VerifyPassword(admin.Password, loginWithEmail.Password)) 
               throw new UnauthorizedAccessException("Invalid credentials");*/

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, admin.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, admin.FullName),
                new Claim(JwtRegisteredClaimNames.Email, admin.Email),
                new Claim(ClaimTypes.Role, UserType.Admins.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });
        }
        else if (loginWithEmail.UserType == UserType.Customers)
        {
            var customer = await _CustomerRep.GetAsync(c => c.Email == loginWithEmail.Email);

            // It's recommended to verify password here before generating token
            // if(!VerifyPassword(customer.PasswordHash, loginWithEmail.Password)) 
            //    throw new UnauthorizedException("Invalid credentials");

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, customer.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, customer.FullName),
                new Claim(JwtRegisteredClaimNames.Email, customer.Email),
                new Claim(ClaimTypes.Role, UserType.Customers.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });
        }

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(5).AddMinutes(_expiryMinutes),
            signingCredentials: credentials  // Added this line to sign the token
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
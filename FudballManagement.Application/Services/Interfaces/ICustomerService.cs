using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.DTOs.Customer;
using FudballManagement.Domain.Entities.Users;
using System.Linq.Expressions;

namespace FudballManagement.Application.Services.Interfaces;
public interface ICustomerService
{
    public Task<CustomerResponseDto> CreateAsync(CustomerCreateDto customerCreate, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(CustomerUpdateDto adminUpdateDto, CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(long CustomerId, CancellationToken cancellationToken);
    public Task<CustomerResponseDto> GetAsync(Expression<Func<Customer, bool>> expression);
    public Task<IEnumerable<CustomerResponseDto>> GetAllAsync();

    public Task<bool> ChangePassword(ChangePasswordDto changePasswordDto, CancellationToken cancellationToken);

    public Task<CustomerProfileResponseDto> GetProfileAsync(long CustomerId , CancellationToken cancellationToken);
    public Task<CustomerProfileResponseDto> UpdateProfileAsync(CustomerProfileDto profileDto, CancellationToken cancellationToken);
}

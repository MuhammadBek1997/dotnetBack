using AutoMapper;
using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.DTOs.Customer;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Extensions;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Users;
using FudballManagement.Domain.Enums;
using FudballManagement.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace FudballManagement.Application.Services.Implamentations;
public class CustomerService : ICustomerService
{
    private readonly IGenericRepository<Customer> _repository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public CustomerService(IGenericRepository<Customer> repository, IFileService fileService, IMapper mapper)
    {
        _repository = repository;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
    {
        var HasUser = await _repository.GetAsync(c => c.PhoneNumber == changePasswordDto.PhoneNumber);
        if (HasUser is null)
            throw new CustomException(404, "Customer not found");

        var CheckPassword = changePasswordDto.OldPassword.Encrypt();
        if (HasUser.Password != CheckPassword || changePasswordDto.NewPassword == changePasswordDto.OldPassword)
            throw new CustomException(400, "Password error");

        HasUser.Password = changePasswordDto.NewPassword.Encrypt();
        HasUser.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(HasUser, cancellationToken);
        return true;
    }

    public async Task<CustomerResponseDto> CreateAsync(CustomerCreateDto customerCreate, CancellationToken cancellationToken)
    {
        var HasCustomer = await _repository.GetAsync(a => a.PhoneNumber == customerCreate.PhoneNumber);

        if (HasCustomer is not null)
            throw new CustomException(404, "Customer Already exist");
        if (customerCreate.Password != customerCreate.ConfirmPassword)
            throw new CustomException(400, "Password did not match");


        customerCreate.Password = customerCreate.Password.Encrypt();
        var Mapping = _mapper.Map<Customer>(customerCreate);
        Mapping.CreatedAt = DateTime.UtcNow;

        if (customerCreate.ProfilePhoto is not null)
        {
            var PhotoPath = await _fileService.SaveProfilePhotoAsync(
                new PhotoUploadDto
                {
                    ProfileMedia = customerCreate.ProfilePhoto,
                    FileName = customerCreate.ProfilePhoto.FileName
                },
                UserType.Customers
                );

            Mapping.ProfilePhoto = PhotoPath;
        }
        var result = await _repository.CreateAsync(Mapping, cancellationToken);
        return _mapper.Map<CustomerResponseDto>(result);
    }

    public async Task<bool> DeleteAsync(long CustomerId, CancellationToken cancellationToken)
    {
        var HasCustomer = await _repository.GetAsync(a => a.Id == CustomerId);
        if (HasCustomer is null)
            throw new CustomException(404, "Customer Not found");

        await _repository.DeleteAsync(a => a.Id == CustomerId, cancellationToken);
        return true;
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
    {
        var Customers = await _repository.GetAllAsync();
        var MappingCustomers = _mapper.Map<List<CustomerResponseDto>>(Customers);

        return MappingCustomers;
    }

    public async Task<CustomerResponseDto> GetAsync(Expression<Func<Customer, bool>> expression)
    {
        var Customer = await _repository.GetAsync(expression);

        var MappingCustomer = _mapper.Map<CustomerResponseDto>(Customer);
        return MappingCustomer;
    }

    public async Task<CustomerProfileResponseDto> GetProfileAsync(long CustomerId, CancellationToken cancellationToken)
    {
        var FindCustomer = await _repository.GetAsync(g => g.Id == CustomerId);
        if (FindCustomer is null)
            throw new CustomException(404, " Customer profile not found");

        return _mapper.Map<CustomerProfileResponseDto>(FindCustomer);
    }

    public async Task<bool> UpdateAsync(CustomerUpdateDto customerUpdate, CancellationToken cancellationToken)
    {
        var Customer = await _repository.GetAsync(a => a.Id == customerUpdate.Id);
        if (Customer is null)
            throw new CustomException(404, "Customer not found");

        _mapper.Map(customerUpdate, Customer);
        Customer.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(Customer, cancellationToken);
        return true;
    }

    public async Task<CustomerProfileResponseDto> UpdateProfileAsync(CustomerProfileDto profileDto, CancellationToken cancellationToken)
    {
        var Customer = await _repository.GetAsync(a => a.Id == profileDto.CustomerId);
        if (Customer == null) throw new CustomException(404, "Customer not found");

        _mapper.Map(profileDto, Customer);
        var updated = await _repository.UpdateAsync(Customer, cancellationToken);
        Customer.UpdatedAt = DateTime.UtcNow;

        return _mapper.Map<CustomerProfileResponseDto>(updated);
    }
}

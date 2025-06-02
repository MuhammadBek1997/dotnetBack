using FudballManagement.Application.DTOs.Order;
using FudballManagement.Application.Exceptions;
using FudballManagement.Application.Services.Interfaces;
using FudballManagement.Domain.Entities.Order;
using FudballManagement.Domain.Entities.Stadiums;
using FudballManagement.Domain.Entities.Users;
using FudballManagement.Infrastructure.DbContexts;
using FudballManagement.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FudballManagement.Application.Services.Implamentations;
public class OrderService : IOrderService
{
    private readonly IGenericRepository<Orders> _orderService;
    private readonly IGenericRepository<Customer> _CustomerService;
    private readonly IGenericRepository<Stadium> _StadiumService;
    private readonly AppDbContext _appDbContext;
    public OrderService(IGenericRepository<Orders> orderService, IGenericRepository<Customer> customerService,
        IGenericRepository<Stadium> stadiumService, AppDbContext app)
    {
        _orderService = orderService;
        _CustomerService = customerService;
        _StadiumService = stadiumService;
        _appDbContext = app;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderDto, CancellationToken cancellationToken)
    {
        var stadium = await _appDbContext.Stadiums.FindAsync(orderDto.StadiumId);
        if (stadium == null)
            return null;

        var duration = orderDto.EndTime - orderDto.StartTime;
        if (duration.TotalHours <= 0)
            return null;

        var overallPrice = stadium.PricePerHour * (decimal)duration.TotalHours;

        var order = new Orders
        {
            CustomerId = orderDto.CustomerId,
            StadiumId = orderDto.StadiumId,
            StartTime = orderDto.StartTime,
            EndTime = orderDto.EndTime,
            OverallPrice = overallPrice,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _appDbContext.Orders.AddAsync(order, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        // Retrieve the created order with related data
        var createdOrder = await _appDbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.Stadiums)
            .FirstOrDefaultAsync(o => o.Id == order.Id, cancellationToken);

        if (createdOrder == null)
            return null;

        return new OrderResponseDto
        {
            Id = createdOrder.Id,
            CustomerId = createdOrder.CustomerId,
            CustomerFullName = createdOrder.Customer.FullName,
            StadiumId = createdOrder.StadiumId,
            StadiumName = createdOrder.Stadiums.StadiumName,
            StartTime = createdOrder.StartTime,
            EndTime = createdOrder.EndTime,
            OverallPrice = createdOrder.OverallPrice
        };
    }

    public async Task<bool> DeleteOrderAsync(long orderId, CancellationToken cancellationToken)
    {
        var order = await _appDbContext.Orders.FindAsync(orderId);

        if (order == null)
            return false;

       var result =  _appDbContext.Orders.Remove(order);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        if(result is null) return false;
        return true;
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
    {
        var orders = await _appDbContext.Orders
            .Include(o => o.Customer)
            .Include(o => o.Stadiums)
            .ToListAsync();

        return orders.Select(o => new OrderResponseDto
        {
            Id = o.Id,
            CustomerId = o.CustomerId,
            CustomerFullName = o.Customer.FullName,
            StadiumId = o.StadiumId,
            StadiumName = o.Stadiums.StadiumName,
            StartTime = o.StartTime,
            EndTime = o.EndTime,
            OverallPrice = o.OverallPrice
        });
    }

    public async Task<OrderResponseDto> GetOrderByIdAsync(long orderId)
    {
        var order = await _appDbContext.Orders
             .Include(o => o.Customer)
             .Include(o => o.Stadiums)
             .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order == null)
            return null;

        return new OrderResponseDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CustomerFullName = order.Customer.FullName,
            StadiumId = order.StadiumId,
            StadiumName = order.Stadiums.StadiumName,
            StartTime = order.StartTime,
            EndTime = order.EndTime,
            OverallPrice = order.OverallPrice
        };
    }
}


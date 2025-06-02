using FudballManagement.Application.DTOs.Order;

namespace FudballManagement.Application.Services.Interfaces;

public  interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto orderDto, CancellationToken cancellationToken);
    Task<bool> DeleteOrderAsync(long orderId, CancellationToken cancellationToken);
    Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
    Task<OrderResponseDto> GetOrderByIdAsync(long orderId);
}

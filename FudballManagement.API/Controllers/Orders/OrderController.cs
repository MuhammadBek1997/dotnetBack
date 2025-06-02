using FudballManagement.Application.DTOs.Order;
using FudballManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FudballManagement.API.Controllers.Orders;
[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto, CancellationToken cancellationToken)
    {
        var createdOrder = await _orderService.CreateOrderAsync(orderDto, cancellationToken);
        if (createdOrder == null)
        {
            return BadRequest("Failed to create order. Please check the provided data.");
        }

        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(long id, CancellationToken cancellationToken)
    {
        var result = await _orderService.DeleteOrderAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound("Order not found.");
        }

        return Ok("Order deleted successfully.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // GET: api/Orders/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrderById(long id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound("Order not found.");
        }

        return Ok(order);
    }
}


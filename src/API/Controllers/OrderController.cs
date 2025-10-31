using Microsoft.AspNetCore.Mvc;
using Tienda.src.Application.Services.Interfaces;
using Tienda.src.Domain.Models;

namespace Tienda.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrders(int userId)
        {
            var orders = await _orderService.GetOrdersAsync(userId);
            return Ok(orders);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await _orderService.CreateOrderAsync(order.UserId);
            return CreatedAtAction(nameof(GetOrders), new { userId = order.UserId }, order);
        }

        // PUT: api/orders/5/status
        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateStatus(int orderId, [FromBody] string status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return NoContent();
        }
    }
}

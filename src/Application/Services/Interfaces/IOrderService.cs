using Tienda.src.Domain.Models;
namespace Tienda.src.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int userId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task CreateOrderAsync(int userId);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}

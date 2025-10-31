using System.Collections.Generic;
using System.Threading.Tasks;   
using Tienda.src.Domain.Models;
namespace Tienda.src.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int userId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}

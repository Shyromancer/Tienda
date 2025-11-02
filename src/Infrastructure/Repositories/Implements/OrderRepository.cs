using Microsoft.EntityFrameworkCore;
using Tienda.src.Domain.Models;
using Tienda.src.Infrastructure.Data;
using Tienda.src.Infrastructure.Repositories.Interfaces;

namespace Tienda.src.Infrastructure.Repositories.Implements
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int userId) =>
            await _context.Orders.Where(o => o.UserId == userId)
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.Product)
                                 .ToListAsync();

        public async Task<Order> GetOrderByIdAsync(int orderId) =>
            await _context.Orders.Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product)
                 .FirstOrDefaultAsync(o => o.Id == orderId)
            ?? throw new InvalidOperationException($"Order with ID {orderId} not found.");

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                var statusProp = order.GetType().GetProperty("Status")
                                 ?? order.GetType().GetProperty("OrderStatus")
                                 ?? order.GetType().GetProperty("StatusId");

                if (statusProp != null)
                {
                    var propType = statusProp.PropertyType;
                    object valueToSet;

                    if (propType == typeof(string))
                    {
                        valueToSet = status;
                    }
                    else if (propType.IsEnum)
                    {
                        valueToSet = System.Enum.Parse(propType, status, ignoreCase: true);
                    }
                    else if (propType == typeof(int) || propType == typeof(int?))
                    {
                        if (int.TryParse(status, out var intVal))
                            valueToSet = intVal;
                        else
                            throw new InvalidOperationException("Cannot convert provided status to int.");
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unsupported status property type: {propType.FullName}");
                    }

                    statusProp.SetValue(order, valueToSet);
                }
                else
                {
                    throw new InvalidOperationException("Order model does not contain a 'Status', 'OrderStatus' or 'StatusId' property. Add one to the Order class or update this repository.");
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}

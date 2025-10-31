

namespace Tienda.src.Infrastructure.Repositories.Interfaces
{
    public interface ICartRepository
    {
    Task AddToCartAsync(int userId, int productId, int quantity);
    Task RemoveFromCartAsync(int userId, int productId);
    Task ClearCartAsync(int userId);
    }
}

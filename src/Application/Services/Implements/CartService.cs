using Tienda.src.Application.Services.Interfaces;
using Tienda.src.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Tienda.src.Application.Services.Implements
{

    public class CartService : ICartService
{
    public Task AddToCartAsync(int userId, int productId, int quantity)
    {
        throw new NotImplementedException();
    }

    public Task ClearCartAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Cart> GetCartAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromCartAsync(int userId, int productId)
    {
        throw new NotImplementedException();
    }
}

}
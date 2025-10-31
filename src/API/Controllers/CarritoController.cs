using Microsoft.AspNetCore.Mvc;
using Tienda.src.Application.Services.Interfaces;
using Tienda.src.Domain.Models;

namespace Tienda.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return cart == null ? NotFound() : Ok(cart);
        }

        // POST: api/cart/5
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(int userId, [FromBody] CartItem item)
        {
            await _cartService.AddToCartAsync(userId, item.ProductId, item.Quantity);
            return NoContent();
        }

        // DELETE: api/cart/5/product/3
        [HttpDelete("{userId}/product/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            await _cartService.RemoveFromCartAsync(userId, productId);
            return NoContent();
        }

        // DELETE: api/cart/5/clear
        [HttpDelete("{userId}/clear")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}

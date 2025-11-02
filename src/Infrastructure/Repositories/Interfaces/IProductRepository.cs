using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.src.Domain.Models;
namespace Tienda.src.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}

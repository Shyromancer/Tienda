using Tienda.src.Domain.Models;

namespace Tienda.src.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task CreateUserAsync(User user, string password);  // Correcta firma
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);  // Correcta firma
    }
}


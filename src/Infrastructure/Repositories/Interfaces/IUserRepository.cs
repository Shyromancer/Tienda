using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tienda.src.Domain.Models;
namespace Tienda.src.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}

using Tienda.src.Application.Services.Interfaces;
using Tienda.src.Domain.Models;
using Tienda.src.Infrastructure.Repositories.Interfaces;

namespace Tienda.src.Application.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Implementación del método CreateUserAsync
        public async Task CreateUserAsync(User user, string password)
        {
            // Lógica para crear el usuario (puedes agregar el hash de la contraseña si es necesario)
            await _userRepository.CreateUserAsync(user, password);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _userRepository.GetUserAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        // Implementación de DeleteUserAsync
        public async Task DeleteUserAsync(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }
    }
}


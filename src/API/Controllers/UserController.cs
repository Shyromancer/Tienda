using Microsoft.AspNetCore.Mvc;
using Tienda.src.Application.Services.Interfaces;
using Tienda.src.Domain.Models;

namespace Tienda.src.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user/5
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _userService.GetUserAsync(userId);
            return user == null ? NotFound() : Ok(user);
        }

        // PUT: api/user/5
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, [FromBody] User user)
        {
            if (userId != user.Id) return BadRequest();
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }
    }
}

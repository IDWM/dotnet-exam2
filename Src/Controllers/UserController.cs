using dotnet_exam2.Src.DTOs;
using dotnet_exam2.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_exam2.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto userDto)
        {
            try
            {
                var createdUser = await _userRepository.CreateUserAsync(userDto);
                return CreatedAtAction(
                    nameof(GetUserById),
                    new { id = createdUser.Id },
                    createdUser
                );
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}

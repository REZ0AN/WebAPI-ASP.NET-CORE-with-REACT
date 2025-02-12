using Microsoft.AspNetCore.Mvc;
using backend.Interfaces;
using backend.Mappers;
using backend.DTOs.User;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET /api/user
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllAsync();
            var userDtos =   users.Select(user => user.ToUserDto());
            return Ok(users);
        }

        // GET /api/user/{id}

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserDto());
        }

        // POST /api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequestDto userRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _repository.CreateAsync(userRequestDto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.ToUserDto());
        }
    }
}
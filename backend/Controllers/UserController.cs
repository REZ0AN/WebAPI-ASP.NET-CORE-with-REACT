using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Mappers;
using backend.DTOs.User;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace backend.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /api/user
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos =   users.Select(user => user.ToUserDto());
            return Ok(users);
        }

        // GET /api/user/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _context.Users.FindAsync(id);
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
            var user = userRequestDto.ToUserModelFromUserRequestDto();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.ToUserDto());
        }
    }
}
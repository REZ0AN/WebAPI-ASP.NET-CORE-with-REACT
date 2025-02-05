using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Mappers;
using backend.DTOs.User;
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
        public IActionResult Get()
        {
            var users = _context.Users.ToList()
                                    .Select(user => user.ToUserDto());
            return Ok(users);
        }

        // GET /api/user/{id}

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUserDto());
        }

        // POST /api/user/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserRequestDto userRequestDto)
        {
            var user = userRequestDto.ToUserModelFromUserRequestDto();
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.ToUserDto());
        }
    }
}
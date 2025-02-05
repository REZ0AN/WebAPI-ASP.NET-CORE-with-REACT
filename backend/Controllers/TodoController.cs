using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Mappers;
using backend.DTOs.Todo;
namespace backend.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /api/todo
        [HttpGet]
        public IActionResult Get()
        {
            var todos = _context.Todos.ToList().Select(todo => todo.ToTodoDto());
            return Ok(todos);
        }

        // GET /api/todo/{id}

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo.ToTodoDto());
        }

        // POST /api/todo/create
        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateTodoRequestDto createTodoRequestDto)
        {
            var todo = createTodoRequestDto.ToTodoFromTodoRequestDto();
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Mappers;
using backend.DTOs.Todo;
using Microsoft.EntityFrameworkCore;
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
        public async  Task<IActionResult> Get()
        {
            var todos =  await _context.Todos.ToListAsync();
            var todoDtos = todos.Select(todo => todo.ToTodoDto());
            return Ok(todos);
        }

        // GET /api/todo/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var todo =  await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo.ToTodoDto());
        }

        // POST /api/todo/create
        [HttpPost("create")]
        public  async Task<IActionResult> Create([FromBody] CreateTodoRequestDto createTodoRequestDto)
        {
            var todo = createTodoRequestDto.ToTodoFromTodoRequestDto();
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
        }

        // PUT /api/todo/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateTodoRequestDto updateTodoRequestDto)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync( todo => todo.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Title = updateTodoRequestDto.Title;
            todo.Description = updateTodoRequestDto.Description;
            todo.IsCompleted = updateTodoRequestDto.IsCompleted;
            
            await _context.SaveChangesAsync();
            return Ok(todo.ToTodoDto());
        }

        // DELETE /api/todo/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
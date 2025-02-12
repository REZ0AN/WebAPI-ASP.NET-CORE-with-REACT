using Microsoft.AspNetCore.Mvc;
using backend.Mappers;
using backend.DTOs.Todo;
using backend.Interfaces;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repository;
        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        // GET /api/todo
        [HttpGet]
        public async  Task<IActionResult> Get()
        {
            var todos =  await _repository.GetAllAsync();
            var todoDtos = todos.Select(todo => todo.ToTodoDto());
            return Ok(todos);
        }

        // GET /api/todo/{id}

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var todo =  await _repository.GetByIdAsync(id);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = createTodoRequestDto.ToTodoFromTodoRequestDto();
            await _repository.CreateAsync(todo);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
        }

        // PUT /api/todo/update/{id}
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateTodoRequestDto updateTodoRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   
            
            var todo = await _repository.UpdateAsync(id, updateTodoRequestDto);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo.ToTodoDto());
        }

        // DELETE /api/todo/delete/{id}
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var todo = await _repository.DeleteAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
# PUT and DELETE verbs with ASP.NET Core WebApi

## PUT and DELETE verbs

In a `RESTful API`, `PUT`, and `DELETE` are essential `HTTP` verbs used for **updating**, and **deleting** existing resources, respectively. In **ASP.NET Core Web API**, these `verbs` are implemented using controller actions that handle **HTTP** requests.

An example:

```c#

    // PUT /api/todo/update/{id}
    [HttpPut("update/{id}")]
    public IActionResult UpdateById([FromRoute] Guid id, [FromBody] UpdateTodoRequestDto updateTodoRequestDto)
    {
        var todo = _context.Todos.FirstOrDefault( todo => todo.Id == id);
        if (todo == null)
        {
            return NotFound();
        }

        todo.Title = updateTodoRequestDto.Title;
        todo.Description = updateTodoRequestDto.Description;
        todo.IsCompleted = updateTodoRequestDto.IsCompleted;
        
        _context.SaveChanges();
        return Ok(todo.ToTodoDto());
    }

    // DELETE /api/todo/delete/{id}
    [HttpDelete("delete/{id}")]
    public IActionResult DeleteById([FromRoute] Guid id)
    {
        var todo = _context.Todos.FirstOrDefault(todo => todo.Id == id);
        if (todo == null)
        {
            return NotFound();
        }
        _context.Todos.Remove(todo);
        _context.SaveChanges();
        return NoContent();
    }
```
You may check for the `Mappers` and `DTOs` directory for the custom `mapper` and `dto` in the `backend` directory. 

For `PUT` and `DELETE` requests, we always need to check if the resource exists before **updating** or **deleting** it.
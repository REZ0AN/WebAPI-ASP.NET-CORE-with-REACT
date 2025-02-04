# DTO and how to define it in ASP.NET Core WebApi

## What is a DTO (Data Transfer Object)?

A `DTO` (**Data Transfer Object**) is an object that carries data between processes, typically between different layers of a backend application. It is a **plain data structure** without business logic, used to `encapsulate` and `transport` data between the server, database, and client.

## Purpose of DTOs in Backend Development

`DTOs` (Data Transfer Objects) are used to **encapsulate**, **filter**, and **transform** data when `transferring` it between different layers (e.g., database, business logic, API).

## Why Use DTOs?

- Security > Prevent exposing sensitive fields (e.g., passwords).
- Performance > Reduce payload size by sending only necessary data.
- Decoupling > Separate internal models from API contracts for flexibility.
- Validation & Transformation > Format or validate incoming/outgoing data.
- Consistency > Standardize API responses across different endpoints.

## Defining DTOs in ASP.NET Core Web API

In **ASP.NET Core Web API**, `DTOs` (**Data Transfer Objects**) are typically defined as plain `C# classes` without business logic. These DTOs are used to shape the request and response data.

### Step-1 

Define a `DTO` **class** separately in a `DTOs` directory to keep it organized. An example from our implementation is given below:

```c#
namespace backend.DTOs.Todo
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UserId { get; set; }
    }
}
```

### Step-2

To use `DTOs` in a `Controller` modify the controller to return `DTOs` instead of database models. An example from our implementation:

```c#
using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Mappers;
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

        [HttpGet]
        public IActionResult Get()
        {
            var todos = _context.Todos.ToList().Select(todo => todo.ToTodoDto());
            return Ok(todos);
        }
    }
}
```
Here, you can see we use some sort of `Mappers` to use DTOs. A `Mapper` is a utility that helps convert one object type to another, typically mapping database models to DTOs (Data Transfer Objects). The most commonly used mapper in ASP.NET Core is `AutoMapper`. But here we are using custom mappers, because in this process the conversion is totally in our hands. 

#### But why use Mappers?

Without `mappers` we cloud do it like below which will work perfectly for us.

```c#
using Microsoft.AspNetCore.Mvc;
using backend.Data;
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

        [HttpGet]
        public IActionResult Get()
        {
            var todos = _context.Todos.ToList().Select(todo => new TodoDto
                                                            {
                                                                Id = todo.Id,
                                                                Title = todo.Title,
                                                                Description = todo.Description,
                                                                IsCompleted = todo.IsCompleted,
                                                                CreatedAt = todo.CreatedAt,
                                                                UserId = todo.UserId
                                                            });
            return Ok(todos);
        }
    }
}
```

What if this `DTO` is need in the whole project for `10 times` how hectic the process it will be?. For this we use mappers. If you use custom mappers these are more flexible than the `AutoMapper`.


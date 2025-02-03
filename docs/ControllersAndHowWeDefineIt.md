# Controllers and how we define it in ASP.NET Core WebAPI

## What Are Controllers ?
Controllers are responsible for handling HTTP requests and returning responses. They act as an intermediary between the client (frontend or API consumer) and the business logic (services, database, etc.).

## Why Do We Use Controllers?

- Handle HTTP Requests → They process GET, POST, PUT, DELETE requests.
- Encapsulate Business Logic → Keep the code structured and organized.
- Return Responses → Controllers send responses in JSON, XML, or other formats.
- Routing → They define endpoints and determine which function runs for each request.

## How we define controllers

Open-up project in `VSCODE` and goto `backend` directory and create `Controllers` directory. Inside this directory we will be creating our controllers for `Todo` and `User` models. Let's explain for `Todo`. 

### Step-1 

Create a class called `TodoController.cs` and write these into the class.

```c#
using Microsoft.AspNetCore.Mvc;
using backend.Data;

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
            var todos = _context.Todos.ToList();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }
    }
}
```
### Explaination of this code block

#### Controller Basics
```c#
using Microsoft.AspNetCore.Mvc;
using backend.Data;
```
- `using Microsoft.AspNetCore.Mvc;` → Imports ASP.NET Core MVC framework to handle HTTP requests.
- `using backend.Data;` → Imports the ApplicationDbContext class, which manages database access.
#### Defining the Controller
```c#
[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
```
- `[ApiController]` → Marks this class as an API controller (automatic model validation, error handling, etc.).
- `[Route("api/todo")]` → Defines the base URL for this controller as `/api/todo`.
- `ControllerBase` → A lightweight controller that does not use **Views** (only for APIs).
#### Dependency Injection (Constructor)
```c#
private readonly ApplicationDbContext _context;
public TodoController(ApplicationDbContext context)
{
    _context = context;
}
```
- `ApplicationDbContext _context;` → Stores the database context.
- `TodoController(ApplicationDbContext context)` → Injects the database context so we can interact with the `database`.

#### GET Endpoint - Fetch All To-Do Items
```c#
[HttpGet]
public IActionResult Get()
{
    var todos = _context.Todos.ToList();
    return Ok(todos);
}
```
- `[HttpGet]` → Handles HTTP GET requests to `/api/todo`.
- `_context.Todos.ToList();` → Fetches all Todo items from the database.
- `return Ok(todos);` → Returns `200 OK` with the list of todos.
#### GET Endpoint - Fetch a Single To-Do by ID
```c#
[HttpGet("{id}")]
public IActionResult GetById([FromRoute] Guid id)
{
    var todo = _context.Todos.Find(id);
    if (todo == null)
    {
        return NotFound();
    }
    return Ok(todo);
}
```
- `[HttpGet("{id}")]` → Handles GET requests like `/api/todo/{id}`.
- `[FromRoute] Guid id` → Extracts the id from the URL.
- `_context.Todos.Find(id);` → Searches for the Todo item in the database.
- `return NotFound();` → Returns `404 Not Found` if no item is found.
- `return Ok(todo);` → Returns `200 OK` with the todo item.

### Step-2 

To make the `controller` workable we need to add something to `Program.cs`.

#### builder.Services.AddControllers()

- Registers the MVC framework in the Dependency Injection (DI) container.
- Enables controller-based routing for handling HTTP requests.
- Must be called before `builder.Build()`.

#### app.MapControllers()

- Maps controller endpoints to the app’s request pipeline.
- Ensures HTTP requests are routed to the correct controller actions.
- Must be called before `app.Run()`.

#### Consequences of Missing These

- Without `AddControllers()`, controllers won’t be recognized.
- Without `MapControllers()`, routes won’t be registered, leading to `404 Not Found` errors.

#### Why Ordering Matters

- `builder.Build()` => Finalizes service registrations and builds the app.
- `app.Run()` => Starts the app; no further `middleware` or `endpoint mappings` can be added.
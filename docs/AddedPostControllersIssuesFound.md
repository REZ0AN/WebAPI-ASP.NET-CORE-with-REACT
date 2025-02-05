# POST verbs with ASP.NET Core WebApi and Postgresql

## POST verb
In a RESTful API, `POST` is an essential `HTTP` verb used for **creating** resources, respectively. In ASP.NET Core Web API, this verb are implemented using controller actions that handle `HTTP` requests.

An example:

```c#
    // POST /api/todo/create
    [HttpPost("create")]
    public IActionResult Create([FromBody] CreateTodoRequestDto createTodoRequestDto)
    {
        var todo = createTodoRequestDto.ToTodoFromTodoRequestDto();
        _context.Todos.Add(todo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
    }
```

You may check for the `Mappers` and `DTOs` directory for the custom `mapper` and `dto` in the `backend` directory.

## An Issue with `DateTime`

While implementing the data models for `todo` and `user`, I initially used `DateTime.Now` for the `CreatedAt` field. However, when testing the `/api/todo/create` endpoint, I encountered an `error`. After investigating, I discovered that **PostgreSQL** expects `timestamps` in `UTC` format. Changing **DateTime.Now** to `DateTime.UtcNow` resolved the issue.
# What is Non-Blocking Code?

`Non-blocking` code means that a function does not stop (block) the execution of a `thread` while waiting for an operation to complete. Instead, it allows the `thread` to do other **work** while the **operation** is in **progress**.

This is important for `scalability` in `high-performance applications`.

# Blocking vs Non-Blocking Code

## Blocking Code

A **blocking** operation stops the execution until it completes. The thread is stuck doing nothing else.
Example:

```c#
public int FetchData()
{
    Console.WriteLine("Fetching data...");
    Thread.Sleep(5000); // Blocks for 5 seconds
    Console.WriteLine("Data fetched.");
    return 42;
}
```
### What happens?

- The thread stops for 5 seconds.
- No other tasks can run on this thread during this time.
- If running in ASP.NET, this would reduce the number of requests the server can handle.

An example from our implemented controller code snippets:

```c#
[HttpPost("create")]
public IActionResult Create([FromBody] CreateTodoRequestDto createTodoRequestDto)
{
    var todo = createTodoRequestDto.ToTodoFromTodoRequestDto();
    _context.Todos.Add(todo);
    _context.SaveChanges();
    return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
}
```

Here, The code is blocking because `_context.SaveChanges()` is a **synchronous** call that halts execution until the database operation completes. This keeps the **request thread** occupied, preventing it from handling other requests, which can lead to **thread pool** `exhaustion` under high load, making the application less responsive and scalable.

## Non-Blocking Code

A **non-blocking** operation allows the `thread` to do other work while waiting for the result.

Example using async/await:

```c#
public async Task<int> FetchDataAsync()
{
    Console.WriteLine("Fetching data...");
    await Task.Delay(5000); // Non-blocking wait
    Console.WriteLine("Data fetched.");
    return 42;
}
```
### What Happens?

- The method starts execution.
- Task.Delay(5000) is asynchronous—it does not block the thread.
- The thread is released back to handle other requests.
- Once the delay completes, execution resumes from where it left off.

How can we convert our `Blocking` code to a `Non-Blocking` code in ASP.NET Core, easily we can say just use `async/await`.

```c#
[HttpPost("create")]
public async Task<IActionResult> Create([FromBody] CreateTodoRequestDto createTodoRequestDto)
{
    var todo = createTodoRequestDto.ToTodoFromTodoRequestDto();
    _context.Todos.Add(todo);
    await _context.SaveChangesAsync(); // Asynchronous, non-blocking call
    return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
}
```
# Why is Non-Blocking Code is Efficient?

- A server can handle more requests using fewer threads.
- No wasted CPU cycles waiting for I/O operations.
- Threads are expensive non-blocking code allows better thread utilization.

So, how we achieve `non-blocking` capabilities by adding only `async/await`. [How they works behind the scene](./AsyncAwait.md)
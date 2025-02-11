# Asynchronous Programming with ASP.NET Core

In C# and ASP.NET Core Web API, asynchronous programming is facilitated by the async and await keywords, which enable developers to write non-blocking code that enhances application scalability and responsiveness.

`async` **Keyword**: When applied to a method, the `async` modifier allows the use of the `await` keyword within that method. It indicates that the method contains **asynchronous operations**.

`await` **Keyword**: The `await` operator is used to **asynchronously** wait for a task to complete without blocking the executing thread. It can only be used within a method marked with `async`.

However, behind this simplicity lies a complex mechanism that the compiler manages on behalf of the developer.

## Compiler Transformation

When the `C# compiler` encounters an `async` method, it transforms it into a `state machine`. This transformation enables the method to pause at `await` expressions and `resume` execution upon the **completion of awaited tasks**. The compiler generates a structure that implements the `IAsyncStateMachine` interface, which includes:

- **State Tracking**: An integer field to keep track of the method's current state.

- **Task Builder**: An `AsyncTaskMethodBuilder` that facilitates the construction and management of the asynchronous task.

- **Awaiters**: Fields to store `awaiters` for each asynchronous operation within the method.

## Execution Flow:

- **Initial Invocation**: When an `async` method is called, it begins executing **synchronously** on the `current thread` until it reaches an `await` expression.

- **Awaiting a Task**: Upon encountering an `await` expression, the method checks if the `awaited` task has **already completed**:

    - **If Completed**: The method proceeds without pausing.

    - **If Not Completed**: The method records its `current state`, sets up a continuation (a delegate to the `MoveNext` method), and returns control to the `caller`. This setup allows the method to `resume execution` once the `awaited task completes`.

- **Resumption**: When the `awaited task` finishes, the stored `continuation` is invoked, restoring the method's state and allowing it to **continue** from where it `left off`.

## Abstractions Provided to Developers:

The `async` and `await` keywords abstract several intricate details:

- **State Management**: Developers are relieved from manually tracking the method's execution state across asynchronous calls.

- **Continuation Handling**: The compiler automatically generates the necessary code to resume method execution after an awaited task completes, eliminating the need for explicit callback management.

- **Exception Propagation**: **Exceptions** within `asynchronous` methods are captured and stored in the **returned task**, allowing developers to handle them using standard exception handling patterns.

## Threads and async/await

### Understanding Threads in C# and ASP.NET Core

#### Thread Pool

`ASP.NET Core` uses a **thread pool** to manage requests efficiently. When a request comes in, `ASP.NET` assigns a worker thread from the thread pool to process it.

#### Synchronous Execution (Blocking Approach)

If the request executes `synchronously`, the worker thread remains `blocked` until the operation (e.g., database query, API call) completes.

#### Asynchronous Execution (Non-Blocking Approach):

With `async/await`, the `thread` is **released back** to the **pool** while waiting for an `I/O-bound` operation to complete. Once the operation finishes, `ASP.NET` **resumes** execution on an **available thread**.

## How async/await Works with Threads

When you mark a method as `async` and use `await`, the compiler transforms it into a **state machine**. Here’s what happens internally:

1. **Request Handling in ASP.NET Core**

A request comes into an ASP.NET Core Web API. The framework assigns a **worker thread** from the `thread pool` to execute the request.

2. **Execution Starts**

The request starts executing **synchronously** on the worker thread until it reaches an **await** statement.

3. **Awaiting an I/O Task**

When the method encounters an **await** operation, such as:

```c#
[HttpPost("create")]
public async Task<IActionResult> Create([FromBody] CreateTodoRequestDto createTodoRequestDto)
{
    var todo = createTodoRequestDto.ToTodoFromTodoRequestDto();
    _context.Todos.Add(todo);

    Console.WriteLine($"Thread Before Await: {Thread.CurrentThread.ManagedThreadId}");

    await _context.SaveChangesAsync(); // Asynchronous, non-blocking call

    Console.WriteLine($"Thread After Await: {Thread.CurrentThread.ManagedThreadId}");

    return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
}
```
The actual `database` call is performed `asynchronously`. Since **database queries** are `I/O-bound` operations (they don't require CPU work, just waiting), the worker thread is **released** back to the thread pool. The request is not blocked, and the `ASP.NET Core` **runtime** can now assign that **thread** to handle another incoming request.

4. **Completion of the I/O Task**

When the **database query** completes, a **thread** from the thread pool is assigned to continue execution. The method resumes from where it left off, processing the remaining logic on a new or the same thread.

You may visualize the execution flow of the upper code snippet like below:

- `Thread x` starts execution and logs `Thread Before Await: x`. 
- At `await _context.SaveChangesAsync();`, The database operation starts in the background.
- `Thread x`is **released** back to the **thread pool**.
- Once the database call completes, `Thread y` (or the same `Thread x`) resumes execution and logs `Thread After Await: y` (or x if the `same thread` is reused).
- Then response is returned to the client.
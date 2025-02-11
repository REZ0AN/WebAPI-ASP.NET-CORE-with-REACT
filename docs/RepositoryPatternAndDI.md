# Repository Pattern and Interfaces

## What is Repository Pattern

The **Repository Pattern** is a design pattern that acts as an `intermediary` between the domain and data mapping layers, providing a collection-like interface for accessing domain objects. Its primary purpose is to `decouple` the **business logic** from the **data access logic**, promoting a **clean separation** of concerns within an application. By abstracting the data access layer, it allows the business logic to remain agnostic of the underlying data storage mechanisms.

## Purpose and Benefits

- **Decoupling:** By abstracting data access, the Repository Pattern decouples the application's business logic from the data access layer. This separation allows developers to modify the data access implementation without affecting the rest of the application. 

- **Testability:** Repositories can be easily mocked or stubbed in unit tests, facilitating more straightforward testing of business logic without relying on actual data sources.

- **Centralized Data Operations:** Repositories centralize data operations, promoting code reuse and consistency across the application. 

## Business Logic and Data Access Logic

This layer, often referred to as the `Business Logic Layer (BLL)`, encapsulates the core operations and rules that define how data is processed and managed to meet the specific requirements of a business domain. It dictates the what and how of data manipulation. On the other hand `Data Access Logic`, also known as the `Data Access Layer (DAL)`, this component is responsible for the direct interaction with data storage systems, such as databases or external services. It manages the how of `data` **retrieval**, **insertion**, **updating**, and **deletion** operations.

## Repository for `TodoController` An Example

To implement the Repository Pattern, you would create an interface defining the operations for the Todo entity:

```c#
using backend.DTOs.Todo;
using backend.Models;

namespace backend.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(Guid id); // Nullable reference type
        Task<Todo> CreateAsync(Todo todo);

        Task<Todo?> UpdateAsync(Guid id, UpdateTodoRequestDto updateTodoRequestDto); // Nullable reference type

        Task<Todo?> DeleteAsync(Guid id); // Nullable reference type
    }
}
```
These two `using backend.DTOs.Todo;` and `using backend.Models;` are here because we have used `DTO` and `Models` which defined for `Todo`.

Then, implement this interface in a concrete repository class:

```c#
using backend.Data;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.DTOs.Todo;

namespace backend.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;
        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Todo>> GetAllAsync()
        {
            var todos = await _context.Todos.ToListAsync();
            return todos;
        }
        
        public async Task<Todo?> GetByIdAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            
            if (todo == null)
            {
                return null;
            }
            return todo;
        }

        public async Task<Todo> CreateAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo?> UpdateAsync(Guid Id, UpdateTodoRequestDto updateTodoRequestDto)
        {
            var todoToUpdate = await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == Id);
            if (todoToUpdate == null)
            {
                return null;
            }
            todoToUpdate.Title = updateTodoRequestDto.Title;
            todoToUpdate.Description = updateTodoRequestDto.Description;
            todoToUpdate.IsCompleted = updateTodoRequestDto.IsCompleted;
            await _context.SaveChangesAsync();
            return todoToUpdate;
        }
        public async Task<Todo?> DeleteAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return null;
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return todo;
        }   
    }
}
```
In this setup, the `TodoRepository` class handles all interactions with the `data source`, allowing the rest of the application to work with Todo entities without concerning itself with the specifics of `data storage or retrieval`. This approach enhances maintainability, testability, and flexibility in the application. And also here this snippet an example of Dependency Injection pattern:

```c#
private readonly ApplicationDbContext _context;
public TodoRepository(ApplicationDbContext context)
{
    _context = context;
}
```
Also in the `TodoController.cs` we can see below snippet

```c#
private readonly ITodoRepository _repository;
public TodoController(ITodoRepository repository)
{
    _repository = repository;
}
```
`Dependency Injection (DI)` plays a pivotal role in promoting **loose coupling** and **enhancing testability**. DI is a `design pattern` that allows an `object` to **receive** its **dependencies** from an `external source` **rather** than creating them **internally**. This approach aligns seamlessly with the Repository Pattern, where `repositories` are injected into `controllers` or `services`, also DbContext is injected into `repositories` known as constructor injection, decoupling the application's components and facilitating easier maintenance and testing.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
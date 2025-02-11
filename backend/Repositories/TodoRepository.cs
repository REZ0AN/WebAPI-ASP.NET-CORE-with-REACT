using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
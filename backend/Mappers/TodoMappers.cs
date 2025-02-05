using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DTOs.Todo;
using backend.Models;
namespace backend.Mappers
{
    public static class TodoMappers
    {
        public static TodoDto ToTodoDto(this Todo todoModel)
        {
            return new TodoDto
            {
                Id = todoModel.Id,
                Title = todoModel.Title,
                IsCompleted = todoModel.IsCompleted,
                Description = todoModel.Description,
                CreatedAt = todoModel.CreatedAt,
                UserId = todoModel.UserId
            };
        }
        public static Todo ToTodoFromTodoRequestDto(this CreateTodoRequestDto createTodoRequestDto)
        {
            return new Todo
            {
                Title = createTodoRequestDto.Title,
                IsCompleted = createTodoRequestDto.IsCompleted,
                Description = createTodoRequestDto.Description,
                UserId = createTodoRequestDto.UserId
            };
        }


        public static Todo ToTodoFromUpdateTodoRequestDto(this UpdateTodoRequestDto updateTodoRequestDto)
        {
            return new Todo
            {
                Title = updateTodoRequestDto.Title,
                IsCompleted = updateTodoRequestDto.IsCompleted,
                Description = updateTodoRequestDto.Description
            };
        }

    }
}
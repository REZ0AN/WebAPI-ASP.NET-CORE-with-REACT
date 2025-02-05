using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.Todo
{
    public class CreateTodoRequestDto
    {
              
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public Guid? UserId { get; set; }
    }
}
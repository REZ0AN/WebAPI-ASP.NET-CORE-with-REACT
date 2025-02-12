using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.Todo
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
        [MaxLength(32, ErrorMessage = "Title must be at most 32 characters long")]
        public required string Title { get; set; }

        [DefaultValue(false)]
        public  bool IsCompleted { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Title must be at least 10 characters long")]
        [MaxLength(250, ErrorMessage = "Title must be at most 250 characters long")]
        public required string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid? UserId { get; set; }
    }
}
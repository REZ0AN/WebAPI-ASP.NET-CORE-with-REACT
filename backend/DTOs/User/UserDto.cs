using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace backend.DTOs.User
{
    public class UserDto
    {
        
        public Guid Id { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
        [MaxLength(32, ErrorMessage = "Username must be at most 32 characters long")]
        public required string Username { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
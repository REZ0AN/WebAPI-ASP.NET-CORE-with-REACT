using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.User
{
    public class CreateUserRequestDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
        [MaxLength(32, ErrorMessage = "Username must be at most 32 characters long")]
        public required string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(32, ErrorMessage = "Password must be at most 32 characters long")]
        public required string Password { get; set; }
    }
}
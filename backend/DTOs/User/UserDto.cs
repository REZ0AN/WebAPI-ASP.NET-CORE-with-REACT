using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.User
{
    public class UserDto
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace  backend.Models 
{
        public class User
    {
    
        public Guid Id { get; set; } = Guid.NewGuid();


        public string Username { get; set; }
    
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}
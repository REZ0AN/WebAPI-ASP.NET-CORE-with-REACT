using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Todo
    {
        
      
        public Guid Id { get; set; } = Guid.NewGuid();

  
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
 
        // Foreign key
        public Guid? UserId { get; set; }

        // Navigation property

        public User? User { get; set; }
    }

}
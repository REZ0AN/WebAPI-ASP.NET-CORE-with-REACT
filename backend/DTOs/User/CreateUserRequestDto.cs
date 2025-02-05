using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.User
{
    public class CreateUserRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DTOs.User;
using backend.Models;

namespace backend.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id); // Nullable reference type
        Task<User> CreateAsync(CreateUserRequestDto userRequestDto);


    }
}
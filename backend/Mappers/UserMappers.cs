using backend.Models;
using backend.DTOs.User;
namespace backend.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                Id = userModel.Id,
                Username = userModel.Username,
                CreatedAt = userModel.CreatedAt
            };
        }
        public static User ToUserModelFromUserRequestDto(this CreateUserRequestDto userRequestDto)
        {
            return new User
            {
                Username = userRequestDto.Username,
                Password = userRequestDto.Password
            };
        }
    }
}
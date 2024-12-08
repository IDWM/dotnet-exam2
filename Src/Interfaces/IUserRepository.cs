using dotnet_exam2.Src.DTOs;

namespace dotnet_exam2.Src.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> CreateUserAsync(CreateUserDto userDto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
    }
}

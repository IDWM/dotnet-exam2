using dotnet_exam2.Src.DTOs;

namespace dotnet_exam2.Src.Interfaces
{
    public interface IUserRepository
    {
        Task<UserResponseDto> CreateUserAsync(UserCreateDto userDto);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int id);
    }
}

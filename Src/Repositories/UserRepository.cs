using AutoMapper;
using AutoMapper.QueryableExtensions;
using dotnet_exam2.Src.Data;
using dotnet_exam2.Src.DTOs;
using dotnet_exam2.Src.Entities;
using dotnet_exam2.Src.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_exam2.Src.Repositories
{
    public class UserRepository(DataContext dataContext, IMapper mapper) : IUserRepository
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
        {
            var gender =
                await _dataContext.Genders.FirstOrDefaultAsync(g => g.Id == userDto.GenderId)
                ?? throw new InvalidOperationException(
                    $"El género con ID {userDto.GenderId} no existe."
                );

            var existingUser = await _dataContext.Users.FirstOrDefaultAsync(u =>
                u.Email == userDto.Email
            );

            if (existingUser != null)
            {
                throw new InvalidOperationException(
                    "Ya existe un usuario con este correo electrónico."
                );
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                GenderId = userDto.GenderId,
                Gender = gender
            };

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Gender = new GenderDto { Id = user.Gender.Id, Name = user.Gender.Name }
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _dataContext
                .Users.Include(u => u.Gender)
                .OrderBy(u => u.Name)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            return await _dataContext
                .Users.Include(u => u.Gender)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}

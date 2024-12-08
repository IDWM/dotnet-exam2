using dotnet_exam2.Src.Data;
using dotnet_exam2.Src.Entities;
using dotnet_exam2.Src.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_exam2.Src.Repositories
{
    public class UserRepository(DataContext dataContext) : IUserRepository
    {
        private readonly DataContext _dataContext = dataContext;

        public async Task<User> CreateUserAsync(User user)
        {
            var gender =
                await _dataContext.Genders.FirstOrDefaultAsync(g => g.Id == user.GenderId)
                ?? throw new InvalidOperationException(
                    $"El género con ID {user.GenderId} no existe."
                );

            var existingUser = await _dataContext.Users.FirstOrDefaultAsync(u =>
                u.Email == user.Email
            );

            if (existingUser != null)
                throw new InvalidOperationException(
                    "Ya existe un usuario con este correo electrónico."
                );

            user.Gender = gender;

            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dataContext
                .Users.Include(u => u.Gender)
                .OrderBy(u => u.Name)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dataContext
                .Users.Include(u => u.Gender)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}

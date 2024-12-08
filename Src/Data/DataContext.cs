using dotnet_exam2.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_exam2.Src.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
        public required DbSet<Gender> Genders { get; set; }
    }
}

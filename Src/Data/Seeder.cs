using Bogus;
using dotnet_exam2.Src.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_exam2.Src.Data
{
    public static class Seeder
    {
        private static readonly List<string> GenderNames =
        [
            "Femenino",
            "Masculino",
            "Prefiero no decirlo",
            "Otro",
        ];

        public static async Task Seed(DataContext dataContext)
        {
            if (await dataContext.Users.AnyAsync())
                return;

            var genders = GenderNames
                .Select(name => new Gender { Name = name, Users = [] })
                .ToList();

            await dataContext.Genders.AddRangeAsync(genders);
            await dataContext.SaveChangesAsync();

            var userFaker = new Faker<User>("es")
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(
                    u => u.BirthDate,
                    f => DateOnly.FromDateTime(f.Date.Past(80, DateTime.Now.AddYears(-18)))
                )
                .RuleFor(u => u.GenderId, f => f.PickRandom(genders).Id)
                .RuleFor(u => u.Gender, (f, u) => genders.First(g => g.Id == u.GenderId));

            var users = userFaker.Generate(50);

            await dataContext.Users.AddRangeAsync(users);
            await dataContext.SaveChangesAsync();
        }
    }
}

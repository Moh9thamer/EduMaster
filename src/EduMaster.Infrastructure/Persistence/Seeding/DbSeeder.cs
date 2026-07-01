using EduMaster.Domain.Levels;
using EduMaster.Domain.Rooms;
using EduMaster.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EduMaster.Infrastructure.Persistence.Seeding
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await SeedLevelsAsync(context);
            await SeedElectiveLevelsAsync(context);
            await SeedRoomsAsync(context);
            await SeedAdminAsync(context);

            await context.SaveChangesAsync();
        }

        private static async Task SeedLevelsAsync(AppDbContext context)
        {
            if (await context.Levels.AnyAsync())
            {
                return;
            }

            var levels = Enumerable.Range(1, 12).Select(i => new Level
            {
                Name = $"Level {i}",
                SortOrder = i
            });

            await context.Levels.AddRangeAsync(levels);
        }

        private static async Task SeedElectiveLevelsAsync(AppDbContext context)
        {
            if (await context.ElectiveLevels.AnyAsync())
            {
                return;
            }

            var electiveLevels = Enumerable.Range(1, 2).Select(i => new ElectiveLevel
            {
                Name = $"Elective Level {i}",
                SortOrder = i
            });

            await context.ElectiveLevels.AddRangeAsync(electiveLevels);
        }

        private static async Task SeedRoomsAsync(AppDbContext context)
        {
            if (await context.Rooms.AnyAsync())
            {
                return;
            }

            var rooms = Enumerable.Range(1, 10).Select(i => new Room
            {
                Name = $"Room {100 + i}",
                Capacity = 30
            });

            await context.Rooms.AddRangeAsync(rooms);
        }

        private static async Task SeedAdminAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            {
                return;
            }

            var admin = new User
            {
                IdentityProviderId = "placeholder|seed-admin",
                Role = UserRole.Admin,
                Name = "System Admin",
                Phone = "0000000000",
                Status = UserStatus.Active
            };

            await context.Users.AddAsync(admin);
        }
    }
}

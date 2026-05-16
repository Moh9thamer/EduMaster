using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.User;

public static class AdminSeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        var existingAdmin = await userManager.GetUsersInRoleAsync("Admin");
        if (existingAdmin.Any()) return;

        var section = config.GetSection("SeedAdmin");

        var admin = new ApplicationUser
        {
            FullName = section["FullName"]!,
            Email = section["Email"],
            UserName = section["Email"],
            PhoneNumber = section["PhoneNumber"]
        };

        var result = await userManager.CreateAsync(admin, section["Password"]!);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(admin, "Admin");
    }
}

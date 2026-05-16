using Application.AcademicYears;
using Application.Grades;
using Application.Semesters;
using Application.Auth;
using Application.Common;
using Application.Users;
using Infrastructure.AcademicYears;
using Infrastructure.Grades;
using Infrastructure.Semesters;
using Infrastructure.Auth;
using Application.Notifications;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Infrastructure.User;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAcademicYearRepository, AcademicYearRepository>();
        services.AddScoped<IAcademicYearService, AcademicYearService>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<IGradeService, GradeService>();
        services.AddScoped<ISemesterRepository, SemesterRepository>();
        services.AddScoped<ISemesterService, SemesterService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IUserService, UserService>();

        // Register Notification services (for Twilio SMS)
        services.AddScoped<ISmsService, SmsNotificationService>();
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}

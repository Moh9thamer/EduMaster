using Domain.AcademicYears;
using Domain.Grades;
using Domain.Semesters;
using Infrastructure.Auth.Models;
using Infrastructure.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<OtpCode> OtpCodes => Set<OtpCode>();
    public DbSet<AcademicYear> AcademicYears => Set<AcademicYear>();
    public DbSet<Grade> Grades => Set<Grade>();
    public DbSet<Semester> Semesters => Set<Semester>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.PhoneNumber)
            .IsUnique();

        builder.Entity<RefreshToken>()
            .HasIndex(r => r.Token)
            .IsUnique();

        builder.Entity<ApplicationUser>()
            .HasQueryFilter(u => u.IsActive);

        builder.Entity<AcademicYear>()
            .HasQueryFilter(a => a.IsActive);

        builder.Entity<Grade>()
            .HasQueryFilter(g => g.IsActive);

        builder.Entity<Semester>()
            .HasQueryFilter(s => s.IsActive);
    }
}


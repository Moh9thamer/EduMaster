using Domain.AcademicYears;
using Domain.Grades;
using Domain.GradingSchemes;
using Domain.Sections;
using Domain.Semesters;
using Domain.Subjects;
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
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<GradingScheme> GradingSchemes => Set<GradingScheme>();

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

        builder.Entity<Section>()
            .HasQueryFilter(s => s.IsActive);

        builder.Entity<Subject>()
            .HasQueryFilter(s => s.IsActive);

        builder.Entity<GradingScheme>()
            .HasQueryFilter(g => g.IsActive);

        builder.Entity<GradingScheme>(b =>
        {
            b.Property(g => g.QuizWeight).HasPrecision(5, 2);
            b.Property(g => g.MidtermWeight).HasPrecision(5, 2);
            b.Property(g => g.FinalWeight).HasPrecision(5, 2);
            b.Property(g => g.AssignmentWeight).HasPrecision(5, 2);
            b.Property(g => g.AttendanceWeight).HasPrecision(5, 2);
        });
    }
}


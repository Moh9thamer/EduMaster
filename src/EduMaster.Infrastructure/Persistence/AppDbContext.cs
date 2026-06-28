using System.Reflection;
using EduMaster.Domain.AcademicYears;
using EduMaster.Domain.Attendance;
using EduMaster.Domain.Closures;
using EduMaster.Domain.CourseMaterials;
using EduMaster.Domain.Courses;
using EduMaster.Domain.DutyRosters;
using EduMaster.Domain.Enrollments;
using EduMaster.Domain.Grades;
using EduMaster.Domain.Levels;
using EduMaster.Domain.Notifications;
using EduMaster.Domain.Rooms;
using EduMaster.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace EduMaster.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<ElectiveLevel> ElectiveLevels { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<SectionSchedule> SectionSchedules { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeAuditLog> GradeAuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }
        public DbSet<Closure> Closures { get; set; }
        public DbSet<ClosureTarget> ClosureTargets { get; set; }
        public DbSet<DutyRoster> DutyRosters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

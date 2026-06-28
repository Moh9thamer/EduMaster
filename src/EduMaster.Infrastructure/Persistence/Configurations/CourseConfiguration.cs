using EduMaster.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class CourseConfiguration : BaseEntityConfiguration<Course>
    {
        public override void Configure(EntityTypeBuilder<Course> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(e => e.MidtermWeight)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.Property(e => e.FinalWeight)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.Property(e => e.HomeworkWeight)
                .IsRequired()
                .HasPrecision(5, 2);
        }
    }
}

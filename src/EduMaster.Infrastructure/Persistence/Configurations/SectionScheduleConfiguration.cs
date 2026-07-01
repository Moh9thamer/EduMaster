using EduMaster.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class SectionScheduleConfiguration : BaseEntityConfiguration<SectionSchedule>
    {
        public override void Configure(EntityTypeBuilder<SectionSchedule> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.DayOfWeek)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(e => e.StartTime)
                .IsRequired();

            builder.Property(e => e.EndTime)
                .IsRequired();

            builder.HasOne(e => e.Section)
                    .WithMany()
                    .HasForeignKey(e => e.SectionId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

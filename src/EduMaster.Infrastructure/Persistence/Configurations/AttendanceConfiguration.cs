using EduMaster.Domain.Attendance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class AttendanceConfiguration : BaseEntityConfiguration<Attendance>
    {
        public override void Configure(EntityTypeBuilder<Attendance> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}

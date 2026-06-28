using EduMaster.Domain.Grades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class GradeAuditLogConfiguration : IEntityTypeConfiguration<GradeAuditLog>
    {
        public void Configure(EntityTypeBuilder<GradeAuditLog> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.OldScore)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.Property(e => e.NewScore)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.Property(e => e.ChangedAt)
                .IsRequired();
        }
    }
}

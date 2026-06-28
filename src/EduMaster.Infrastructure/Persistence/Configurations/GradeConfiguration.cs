using EduMaster.Domain.Grades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class GradeConfiguration : BaseEntityConfiguration<Grade>
    {
        public override void Configure(EntityTypeBuilder<Grade> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Score)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.Property(e => e.Published)
                .HasDefaultValue(false);
        }
    }
}

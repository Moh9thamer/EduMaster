using EduMaster.Domain.Closures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class ClosureTargetConfiguration : IEntityTypeConfiguration<ClosureTarget>
    {
        public void Configure(EntityTypeBuilder<ClosureTarget> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Closure)
                .WithMany(c => c.ClosureTargets)
                .HasForeignKey(e => e.ClosureId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Section)
                .WithMany()
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(e => !e.Closure.IsDeleted && !e.Section.IsDeleted);
        }
    }
}

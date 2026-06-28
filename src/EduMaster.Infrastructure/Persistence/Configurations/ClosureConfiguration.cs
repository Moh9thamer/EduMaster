using EduMaster.Domain.Closures;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class ClosureConfiguration : BaseEntityConfiguration<Closure>
    {
        public override void Configure(EntityTypeBuilder<Closure> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Scope)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}

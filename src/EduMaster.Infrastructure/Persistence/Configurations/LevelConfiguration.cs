using EduMaster.Domain.Levels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class LevelConfiguration : BaseEntityConfiguration<Level>
    {
        public override void Configure(EntityTypeBuilder<Level> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.SortOrder)
                .IsRequired();
        }
    }
}

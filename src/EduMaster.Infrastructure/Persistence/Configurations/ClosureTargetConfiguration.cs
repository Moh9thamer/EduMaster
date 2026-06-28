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
        }
    }
}

using EduMaster.Domain.DutyRosters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class DutyRosterConfiguration : BaseEntityConfiguration<DutyRoster>
    {
        public override void Configure(EntityTypeBuilder<DutyRoster> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Date)
                .IsRequired();

            builder.HasOne(e => e.Manager)
                .WithMany()
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}


using EduMaster.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.IdentityProviderId)
                .IsRequired()
                .HasMaxLength(100);
        
            builder.Property(e => e.Role)
                      .IsRequired()
                      .HasConversion<string>();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Phone)
                .HasMaxLength(20);

            builder.Property(e => e.PhotoUrl)
                .HasMaxLength(500);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(e => e.Level)
                .WithMany()
                .HasForeignKey(e => e.LevelId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using EduMaster.Domain.CourseMaterials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduMaster.Infrastructure.Persistence.Configurations
{
    public class CourseMaterialConfiguration : BaseEntityConfiguration<CourseMaterial>
    {
        public override void Configure(EntityTypeBuilder<CourseMaterial> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.FileUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.UploadedBy)
                .WithMany()
                .HasForeignKey(e => e.UploadedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using EduMaster.Domain.Common;
using EduMaster.Domain.Courses;
using EduMaster.Domain.Users;

namespace EduMaster.Domain.CourseMaterials
{
    public class CourseMaterial : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;

        public Guid UploadedById { get; set; }
        public User UploadedBy { get; set; } = null!;

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}

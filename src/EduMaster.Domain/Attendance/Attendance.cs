using EduMaster.Domain.Common;
using EduMaster.Domain.Users;
using EduMaster.Domain.Courses;

namespace EduMaster.Domain.Attendance
{
    public class Attendance : BaseEntity
    {
        public DateOnly Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public Guid StudentId { get; set; }
        public User Student { get; set; } = null!;
        public Guid SectionId { get; set; }
        public Section Section { get; set; } = null!;
    }
}

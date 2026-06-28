using EduMaster.Domain.Common;
using EduMaster.Domain.Rooms;
using EduMaster.Domain.Users;

namespace EduMaster.Domain.Courses
{
    public class Section : BaseEntity
    {
        public int SectionNumber { get; set; }
        public int Capacity { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public Guid TeacherId { get; set; }
        public User Teacher { get; set; } = null!;

        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
    }
}

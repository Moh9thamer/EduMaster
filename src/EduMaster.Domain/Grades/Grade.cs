using EduMaster.Domain.Common;
using EduMaster.Domain.Users;
using EduMaster.Domain.Courses;

namespace EduMaster.Domain.Grades
{
    public class Grade : BaseEntity
    {
        public decimal Score { get; set; }

        public bool Published { get; set; }
        public Guid StudentId { get; set; }
        public User Student { get; set; } = null!;

        public Guid AssignmentId { get; set; }

        public Assignment Assignment { get; set; } = null!;
    }
}

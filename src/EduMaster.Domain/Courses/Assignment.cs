using EduMaster.Domain.Common;

namespace EduMaster.Domain.Courses
{
    public class Assignment : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public AssignmentType Type { get; set; }
        public decimal MaxGrade { get; set; }
        public DateOnly? DueDate { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}

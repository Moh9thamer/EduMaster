using EduMaster.Domain.AcademicYears;
using EduMaster.Domain.Common;
using EduMaster.Domain.Levels;

namespace EduMaster.Domain.Courses
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CourseType Type { get; set; }
        public decimal MidtermWeight { get; set; }
        public decimal FinalWeight { get; set; }
        public decimal HomeworkWeight { get; set; }

        public Guid SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;

        public Guid? LevelId { get; set; }
        public Level? Level { get; set; }

        public Guid? ElectiveLevelId { get; set; }
        public ElectiveLevel? ElectiveLevel { get; set; }
    }
}

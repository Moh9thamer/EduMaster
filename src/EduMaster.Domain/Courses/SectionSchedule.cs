using EduMaster.Domain.Common;

namespace EduMaster.Domain.Courses
{
    public class SectionSchedule : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public Guid SectionId { get; set; }
        public Section Section { get; set; } = null!;
    }
}

using EduMaster.Domain.Common;

namespace EduMaster.Domain.AcademicYears
{
    public class Semester : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SemesterStatus Status { get; set; }

        public Guid AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; } = null!;
    }
}

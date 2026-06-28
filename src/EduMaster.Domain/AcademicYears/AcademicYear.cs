using EduMaster.Domain.Common;

namespace EduMaster.Domain.AcademicYears
{
    public class AcademicYear : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AcademicYearStatus Status { get; set; }

    }
}

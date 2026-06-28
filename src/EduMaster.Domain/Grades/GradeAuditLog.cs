using EduMaster.Domain.Users;

namespace EduMaster.Domain.Grades
{
    public class GradeAuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal OldScore { get; set; }
        public decimal NewScore { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public Guid ChangedById { get; set; }
        public User ChangedBy { get; set; } = null!;

        public Guid GradeId { get; set; }
        public Grade Grade { get; set; } = null!;
    }
}

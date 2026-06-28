using EduMaster.Domain.Courses;
namespace EduMaster.Domain.Closures
{
    public class ClosureTarget 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClosureId { get; set; }
        public Closure Closure { get; set; } = null!;

        public Guid SectionId { get; set; }

        public Section Section { get; set; } = null!;
    }
}

using EduMaster.Domain.Common;

namespace EduMaster.Domain.Closures
{
    public class Closure : BaseEntity
    {
        public DateOnly Date { get; set; }

        public ClosureScope Scope { get; set; }

        public ICollection<ClosureTarget> ClosureTargets { get; set; } = [];
    }
}

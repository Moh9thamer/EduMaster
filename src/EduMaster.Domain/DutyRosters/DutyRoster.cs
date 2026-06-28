using EduMaster.Domain.Common;
using EduMaster.Domain.Users;

namespace EduMaster.Domain.DutyRosters
{
    public class DutyRoster : BaseEntity
    {
        public DateOnly Date { get; set; }

        public Guid ManagerId { get; set; }
        public User Manager { get; set; } = null!;
    }
}

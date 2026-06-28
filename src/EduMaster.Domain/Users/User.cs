using EduMaster.Domain.Common;
using EduMaster.Domain.Levels;

namespace EduMaster.Domain.Users
{
    public class User : BaseEntity
    {
        public string IdentityProviderId { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }

        public UserStatus Status { get; set; }

        public Guid? LevelId { get; set; }
        public Level? Level { get; set; } 
    }
}

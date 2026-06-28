using EduMaster.Domain.Common;
using EduMaster.Domain.Users;

namespace EduMaster.Domain.Notifications
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public bool IsRead { get; set; }

        public Guid RecipientId { get; set; }
        public User Recipient { get; set; } = null!;
    }
}

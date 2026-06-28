using EduMaster.Domain.Common;

namespace EduMaster.Domain.Rooms
{
    public class Room : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int Capacity { get; set; }
    }
}

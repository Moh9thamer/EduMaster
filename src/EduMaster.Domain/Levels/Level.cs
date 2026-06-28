using EduMaster.Domain.Common;

namespace EduMaster.Domain.Levels
{
    public class Level : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}

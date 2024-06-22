using EducationSystem.Shared.Models.ClassDto;

namespace EducationSystem.Shared.Relationships
{
    public class ItemInCurriculumDto
    {
        public int ItemId { get; set; }
        public int CurriculumId { get; set; }
        public ItemDto LinkItem { get; set; } = new();
        public CurriculumDto LinkCurriculum { get; set; } = new();
        public int NumberOfHours { get; set; }
    }
}

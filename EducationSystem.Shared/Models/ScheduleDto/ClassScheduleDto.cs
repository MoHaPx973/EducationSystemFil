using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.Models.Helper;

namespace EducationSystem.Shared.Models.ScheduleDto
{
    public class ClassScheduleDto:SharedModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Description { get; set; }
        public SchoolClassDto LinkClass { get; set; } = new();
    }
}

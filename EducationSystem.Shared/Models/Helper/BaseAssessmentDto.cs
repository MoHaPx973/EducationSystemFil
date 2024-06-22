
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.Relationships;
using System.ComponentModel;

namespace EducationSystem.Shared.Models.Helper
{
    public class BaseAssessmentDto
    {
        public int Id { get; set; }
        public PersonDto? Student { get; set; } = new();
        public PersonDto? Teacher { get; set; } = new();
        public SchoolClassDto? StudentClass { get; set; } = new();
        public ItemInCurriculumDto? LinkItem { get; set; }
        public int Point { get; set; }
        public string? Description { get; set; }
    }
}

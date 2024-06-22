using EducationSystem.Shared.Models.Helper;

namespace EducationSystem.Shared.Models.ClassDto
{
    public class SchoolClassDto:SharedModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Letter { get; set; } = string.Empty;
        public int YearFormation { get; set; }
        public CurriculumDto? LinkCurriculum { get; set; } = new();
    }
}

using EducationSystem.Shared.Models.Helper;

namespace EducationSystem.Shared.Models.ClassDto
{
    public class CurriculumDto : SharedModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int YearFormation { get; set; } 
        //количество семестров в году
        public int SystemTeaching { get; set; }
        public string? Description { get; set; }
    }
}

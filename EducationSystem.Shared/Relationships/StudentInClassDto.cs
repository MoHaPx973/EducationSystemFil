using EducationSystem.Shared.Models;
using EducationSystem.Shared.Models.ClassDto;

namespace EducationSystem.Shared.Relationships
{
    public class StudentInClassDto
    {
        public StudentInClassDto()
        {
            IsStuding = true;
        }

        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public PersonDto Student { get; set; } = new();
        public SchoolClassDto SchoolClass { get; set; } = new();
        public bool IsStuding { get; set; }
    }
}

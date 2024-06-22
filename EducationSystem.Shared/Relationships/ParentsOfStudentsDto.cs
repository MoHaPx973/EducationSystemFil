using EducationSystem.Shared.Models;

namespace EducationSystem.Shared.Relationships
{
    public class ParentsOfStudentsDto
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public PersonDto Parent { get; set; } = new();
        public PersonDto Student { get; set; } = new();
    }
}

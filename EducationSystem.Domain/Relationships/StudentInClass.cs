using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;

namespace EducationSystem.Domain.Relationships
{
	public class StudentInClass
	{
        public StudentInClass() { }
        public StudentInClass(Person student, SchoolClass schoolClass)
        {
            StudentId = student.Id;
            ClassId = schoolClass.Id;
            Student = student;
            LinkClass = schoolClass;
        }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public Person Student { get; set; } = new();
        public SchoolClass LinkClass { get; set; } = new();
        public bool IsStuding { get; set; } = true;
    }
}

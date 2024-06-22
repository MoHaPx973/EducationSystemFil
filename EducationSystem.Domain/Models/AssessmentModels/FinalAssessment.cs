
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Models.Helper;
using EducationSystem.Domain.Relationships;

namespace EducationSystem.Domain.Models.AssessmentModels
{
    public class FinalAssessment:Assessment
    {
        public FinalAssessment() { }
        public FinalAssessment(Person student, Person teacher, SchoolClass studentClass, ItemInCurriculum item, int point, int systemTeachingNumber, string description)
        {
            Student = student;
            Teacher = teacher;
            StudentClass = studentClass;
            LinkItem = item;
            Description = description;
            SystemTeachingNumber = systemTeachingNumber; 
            Point = point;
        }
    }
}

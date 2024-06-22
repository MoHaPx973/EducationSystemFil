using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Models.Helper;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Domain.Relationships;

namespace EducationSystem.Domain.Models.AssessmentModels
{
    public class LessonAssessment:Assessment
    {
        public LessonAssessment() { }
        
        public LessonAssessment(Person student, Person teacher, SchoolClass studentClass,ItemInCurriculum item, int point, LessonInSсhedule lesson)
        {
            Student = student;
            Teacher = teacher;
            StudentClass = studentClass;
            LinkItem = item;
            Point = point;
            Lesson = lesson;
        }
        public LessonInSсhedule Lesson
        {
            get => _lesson;
            set
            {
                if ((_lesson != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки урока");
                }
                _lesson = value;
            }
        }
        private LessonInSсhedule _lesson = new();
    }
}

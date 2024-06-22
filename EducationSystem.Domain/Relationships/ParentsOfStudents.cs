using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;

namespace EducationSystem.Domain.Relationships
{
    public class ParentsOfStudents
    {
        public ParentsOfStudents() { }

        public ParentsOfStudents(Person parent, Person student)
        {
            ParentId = parent.Id;
            StudentId = student.Id;
            Parent = parent;
            Student = student;
        }

        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public Person Parent
        {
            get => _parent;
            set
            {
                if ((_parent != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки родителя");
                }
                _parent = value;
            }
        }
        public Person Student
        {
            get => _student;
            set
            {
                if ((_student != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки ученика");
                }
                _student = value;
            }
        }
        private Person _parent = new();
        private Person _student = new();
    }
}

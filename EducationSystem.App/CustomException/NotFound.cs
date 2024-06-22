
namespace EducationSystem.App.CustomException
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(string message)
        : base(message)
        { }
    }
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException(string message)
        : base(message)
        { }
    }
    public class ClassNotFoundException : Exception
    {
        public ClassNotFoundException(string message)
        : base(message)
        { }
    }
    public class CurriculumNotFoundException : Exception
    {
        public CurriculumNotFoundException(string message)
        : base(message)
        { }
    }
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message)
        : base(message)
        { }
    }
    public class TimeNotFoundException : Exception
    {
        public TimeNotFoundException(string message)
        : base(message)
        { }
    }
    public class ClassScheduleNotFoundException : Exception
    {
        public ClassScheduleNotFoundException(string message)
        : base(message)
        { }
    }
    public class LessonTimeNotFoundException : Exception
    {
        public LessonTimeNotFoundException(string message)
        : base(message)
        { }
    }
    public class ClassroomNotFoundException : Exception
    {
        public ClassroomNotFoundException(string message)
        : base(message)
        { }
    }
    public class StudyDayNotFoundException : Exception
    {
        public StudyDayNotFoundException(string message)
        : base(message)
        { }
    }
    public class PersonRoleNotCorrect : Exception
    {
        public PersonRoleNotCorrect(string message)
        : base(message)
        { }
    }
    public class StudentNotInClass : Exception
    {
        public StudentNotInClass(string message)
        : base(message)
        { }
    }
    public class ItemNotInCurriculum : Exception
    {
        public ItemNotInCurriculum(string message)
        : base(message)
        { }
    }
}

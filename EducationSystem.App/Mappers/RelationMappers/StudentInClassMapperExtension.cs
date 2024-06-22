using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Relationships;

namespace EducationSystem.App.Mappers.RelationMappers
{
    static class StudentInClassMapperExtension
    {
        static public StudentInClassDto? ToDto(this StudentInClass? item)
        {
            if (item != null)
            {
                return new StudentInClassDto
                {
                    StudentId = item.StudentId,
                    ClassId = item.ClassId,
                    Student = item.Student.ToDto(),
                    SchoolClass = item.LinkClass.ToDto(),
                    IsStuding = item.IsStuding
                };
            }
            return null;
        }

        static public StudentInClass? ToEntity(this StudentInClassDto? item)
        {
            if (item != null)
            {
                return new StudentInClass
                {
                    StudentId = item.StudentId,
                    ClassId = item.ClassId,
                    Student = item.Student.ToEntity(),
                    LinkClass = item.SchoolClass.ToEntity(),
                    IsStuding = item.IsStuding
                };
            }
            return null;
        }
    }
}

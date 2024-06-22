using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Relationships;

namespace EducationSystem.App.Mappers.RelationMappers
{
    static class ParentsOfStudentsMapperExtension
    {
        static public ParentsOfStudentsDto? ToDto(this ParentsOfStudents? item)
        {
            if (item != null)
            {
                return new ParentsOfStudentsDto
                {
                    ParentId = item.ParentId,
                    StudentId = item.StudentId,
                    Parent = item.Parent.ToDto(),
                    Student = item.Student.ToDto()
                };
            }
            return null;
        }

        static public ParentsOfStudents? ToEntity(this ParentsOfStudentsDto? item)
        {
            if (item != null)
            {
                return new ParentsOfStudents
                {
                    ParentId = item.ParentId,
                    StudentId = item.StudentId,
                    Parent = item.Parent.ToEntity(),
                    Student = item.Student.ToEntity()
                };
            }
            return null;
        }
    }
}

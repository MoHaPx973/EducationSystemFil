using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.App.Mappers.ModelsMappers.AssessmentMapper;
using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.App.Mappers.RelationMappers;
using EducationSystem.Domain.Models.AssessmentModels;
using EducationSystem.Shared.Models.AssessmentDto;

namespace EducationSystem.App.Mappers.ModelsMappers.AssessmentMapper
{
    static class FinalAssessmentMapperExtension
    {
        static public FinalAssessmentDto? ToDto(this FinalAssessment? item)
        {
            if (item != null)
            {
                return new FinalAssessmentDto
                {
                    Id = item.Id,
                    Student = item.Student.ToDto(),
                    Teacher = item.Teacher.ToDto(),
                    StudentClass = item.StudentClass.ToDto(),
                    LinkItem = item.LinkItem.ToDto(),
                    Description = item.Description,
                    Point = item.Point
                };
            }
            return null;
        }

        static public FinalAssessment? ToEntity(this FinalAssessmentDto? item)
        {
            if (item != null)
            {
                return new FinalAssessment
                {
                    Id = item.Id,
                    Student = item.Student.ToEntity(),
                    Teacher = item.Teacher.ToEntity(),
                    StudentClass = item.StudentClass.ToEntity(),
                    LinkItem = item.LinkItem.ToEntity(),
                    Description = item.Description,
                    Point = item.Point
                };
            }
            return null;
        }
    }
}

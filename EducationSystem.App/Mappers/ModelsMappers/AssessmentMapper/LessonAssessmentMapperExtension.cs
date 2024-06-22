using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.App.Mappers.ModelsMappers.AssessmentMapper;
using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers;
using EducationSystem.App.Mappers.RelationMappers;
using EducationSystem.Domain.Models.AssessmentModels;
using EducationSystem.Shared.Models.AssessmentDto;

namespace EducationSystem.App.Mappers.ModelsMappers.AssessmentMapper
{
    static class LessonAssessmentMapperExtension
    {
        static public LessonAssessmentDto? ToDto(this LessonAssessment? item)
        {
            if (item != null)
            {
                return new LessonAssessmentDto
                {
                    Id = item.Id,
                    Student = item.Student.ToDto(),
                    Teacher = item.Teacher.ToDto(),
                    StudentClass = item.StudentClass.ToDto(),
                    LinkItem = item.LinkItem.ToDto(),
                    Point = item.Point,
                    Lesson = item.Lesson.ToDto(),
                    
                };
            }
            return null;
        }

        static public LessonAssessment? ToEntity(this LessonAssessmentDto? item)
        {
            if (item != null)
            {
                return new LessonAssessment
                {
                    Id = item.Id,
                    Student = item.Student.ToEntity(),
                    Teacher = item.Teacher.ToEntity(),
                    StudentClass = item.StudentClass.ToEntity(),
                    LinkItem = item.LinkItem.ToEntity(),
                    Point = item.Point,
                    Lesson = item.Lesson.ToEntity()
                };
            }
            return null;
        }
    }
}

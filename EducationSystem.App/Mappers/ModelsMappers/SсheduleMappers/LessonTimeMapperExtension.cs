using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models.ScheduleDto;

namespace EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers
{
    static class LessonTimeMapperExtension
    {
        static public LessonTimeDto? ToDto(this LessonTime? item)
        {
            if (item != null)
            {
                return new LessonTimeDto
                {
                    Number = item.Number,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime
                };
            }
            return null;
        }

        static public LessonTime? ToEntity(this LessonTimeDto? item)
        {
            if (item != null)
            {
                return new LessonTime
                {
                    Number = item.Number,
                    _startTime = item.StartTime,
                    _endTime = item.EndTime
                };
            }
            return null;
        }
    }
}

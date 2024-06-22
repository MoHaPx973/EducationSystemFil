using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models.ScheduleDto;

namespace EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers
{
    static class LessonsInSсheduleMapperExtension
    {
        static public LessonInSсheduleDto? ToDto(this LessonInSсhedule? item)
        {
            if (item != null)
            {
                return new LessonInSсheduleDto
                {
                    Id = item.Id,
                    LinkItem = item.LinkItem.ToDto(),
                    Teacher = item.Teacher.ToDto(),
                    LinkSchedule = item.LinkSchedule.ToDto(),
                    RoomNumber = item.RoomNumber.ToDto(),
                    Time = item.Time.ToDto(),
                    Day = item.Day.ToDto()
                };
            }
            return null;
        }

        static public LessonInSсhedule? ToEntity(this LessonInSсheduleDto? item)
        {
            if (item != null)
            {
                return new LessonInSсhedule
                {
                    Id = item.Id,
                    LinkItem = item.LinkItem.ToEntity(),
                    Teacher = item.Teacher.ToEntity(),
                    LinkSchedule = item.LinkSchedule.ToEntity(),
                    RoomNumber = item.RoomNumber.ToEntity(),
                    Time = item.Time.ToEntity(),
                    //Day = item.Day.ToEntity()
                };
            }
            return null;
        }
    }
}

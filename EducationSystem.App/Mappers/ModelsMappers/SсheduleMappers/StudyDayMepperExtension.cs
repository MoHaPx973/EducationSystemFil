using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models.ScheduleDto;

namespace EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers
{
    static class StudyDayMepperExtension
    {
        static public StudyDayDto? ToDto(this StudyDay? item)
        {
            if (item != null)
            {
                return new StudyDayDto
                {
                    Id = item.Id,
                    Date = item.Date,
                    DayWeekName = item.DayWeekName,
                    DayWeekNumber = item.DayWeekNumber,
                    WeekNumber = item.WeekNumber
                };
            }
            return null;
        }

        static public StudyDay? ToEntity(this StudyDayDto? item)
        {
            if (item != null)
            {
                return new StudyDay
                {
                    Id = item.Id,
                    Date = item.Date,
                    DayWeekName = item.DayWeekName,
                    DayWeekNumber = item.DayWeekNumber,
                    WeekNumber = item.WeekNumber
                };
            }
            return null;
        }
    }
}

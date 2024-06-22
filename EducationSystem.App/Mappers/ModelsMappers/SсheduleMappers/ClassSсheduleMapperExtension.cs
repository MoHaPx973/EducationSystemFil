using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models.ScheduleDto;

namespace EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers
{
    static public class ClassSсheduleMapperExtension
    {
        static public ClassScheduleDto? ToDto(this ClassSchedule? item)
        {
            if (item != null)
            {
                return new ClassScheduleDto
                {
                    Id = item.Id,
                    Number = item.Number,
                    Description = item.Description,
                    LinkClass = item.LinkClass.ToDto()
                };
            }
            return null;
        }

        static public ClassSchedule? ToEntity(this ClassScheduleDto? item)
        {
            if (item != null)
            {
                return new ClassSchedule
                {
                    Id = item.Id,
                    Number = item.Number,
                    Description = item.Description,
                    LinkClass = item.LinkClass.ToEntity()
                };
            }
            return null;
        }
    }
}

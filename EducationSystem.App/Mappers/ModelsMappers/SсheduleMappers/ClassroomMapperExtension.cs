using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models.ScheduleDto;

namespace EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers
{
    static class ClassroomMapperExtension
    {
        static public ClassroomDto? ToDto(this Classroom? item)
        {
            if (item != null)
            {
                return new ClassroomDto
                {
                   Number = item.Number,
                   Description = item.Description
                };
            }
            return null;
        }

        static public Classroom? ToEntity(this ClassroomDto? item)
        {
            if (item != null)
            {
                return new Classroom
                {
                    Number = item.Number,
                    Description = item.Description
                };
            }
            return null;
        }
    }
}

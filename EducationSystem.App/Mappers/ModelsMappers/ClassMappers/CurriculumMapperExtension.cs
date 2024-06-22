using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Shared.Models.ClassDto;


namespace EducationSystem.App.Mappers.ModelsMappers.ClassMappers
{
    static class CurriculumMapperExtension
    {
        static public CurriculumDto? ToDto(this Curriculum? item)
        {
            if (item != null)
            {
                return new CurriculumDto
                {
                    Id=item.Id,
                    Number = item.Number,
                    YearFormation = item.YearFormation,
                    SystemTeaching = item.SystemTeaching,
                    Description = item.Description,
                    DateCreate = item.DateCreate,
                    DateUpdate = item.DateUpdate,
                    DateHidden = item.DateHidden,
                    IsHidden = item.IsHidden
                };
            }
            return null;
        }

        static public Curriculum? ToEntity(this CurriculumDto? item)
        {
            if (item != null)
            {
                return new Curriculum
                {
                    Id = item.Id,
                    Number = item.Number,
                    YearFormation = item.YearFormation,
                    SystemTeaching = item.SystemTeaching,
                    Description = item.Description,
                    DateCreate = item.DateCreate,
                    DateUpdate = item.DateUpdate,
                    DateHidden = item.DateHidden,
                    IsHidden = item.IsHidden
                };
            }
            return null;
        }
    }
}

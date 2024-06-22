using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Shared.Models.ClassDto;

namespace EducationSystem.App.Mappers.ModelsMappers.ClassMappers
{
    static class SchoolClassMapperExtension
    {
        static public SchoolClassDto? ToDto(this SchoolClass? item)
        {
            if (item != null)
            {
                return new SchoolClassDto
                {
                    Id = item.Id,
                    Number = item.Number,
                    Letter = item.Letter,
                    YearFormation = item.YearFormation,
                    LinkCurriculum = item.LinkCurriculum.ToDto(),
                    DateCreate = item.DateCreate,
                    DateUpdate = item.DateUpdate,
                    DateHidden = item.DateHidden,
                    IsHidden = item.IsHidden
                };
            }
            return null;
        }

        static public SchoolClass? ToEntity(this SchoolClassDto? item)
        {
            if (item != null)
            {
                return new SchoolClass
                {
                    Id = item.Id,
                    Number = item.Number,
                    Letter = item.Letter,
                    YearFormation = item.YearFormation,
                    LinkCurriculum = item.LinkCurriculum.ToEntity(),
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

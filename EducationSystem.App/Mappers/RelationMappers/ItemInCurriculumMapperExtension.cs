using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Relationships;

namespace EducationSystem.App.Mappers.RelationMappers
{
    static class ItemInCurriculumMapperExtension
    {
        static public ItemInCurriculumDto? ToDto(this ItemInCurriculum? item)
        {
            if (item != null)
            {
                return new ItemInCurriculumDto
                {
                    ItemId = item.ItemId,
                    CurriculumId = item.CurriculumId,
                    LinkItem = item.LinkItem.ToDto(),
                    LinkCurriculum = item.LinkCurriculum.ToDto(),
                    NumberOfHours = item.NumberOfHours,
                };
            }
            return null;
        }

        static public ItemInCurriculum? ToEntity(this ItemInCurriculumDto? item)
        {
            if (item != null)
            {
                return new ItemInCurriculum
                {
                    ItemId = item.ItemId,
                    CurriculumId = item.CurriculumId,
                    LinkItem = item.LinkItem.ToEntity(),
                    LinkCurriculum = item.LinkCurriculum.ToEntity(),
                    NumberOfHours = item.NumberOfHours,
                };
            }
            return null;
        }
    }
}

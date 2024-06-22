using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Shared.Models.ClassDto;

namespace EducationSystem.App.Mappers.ModelsMappers.ClassMappers
{
    static class ItemMapperExtension
    {
        static public ItemDto? ToDto(this Item? item)
        {
            if (item != null)
            {
                return new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                };
            }
            return null;
        }

        static public Item? ToEntity(this ItemDto? item)
        {
            if (item != null)
            {
                return new Item
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                };
            }
            return null;
        }
    }
}

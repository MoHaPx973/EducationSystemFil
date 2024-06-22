using EducationSystem.Domain.Files;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Files;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Mappers.FilePathMappers
{
    static public class FilePathMapper
    {
        static public FilePathDto? ToDto(this FilePath? item)
        {
            if (item != null)
            {
                return new FilePathDto
                {
                    Id = item.Id,
                    Path = item.Path,
                    PersonId = item.PersonId,
                    Name = item.Name,
                    ClassNumber = item.ClassNumber,
                    ItemNumber = item.ItemNumber,
                    TypeId = item.TypeId
                };
            }
            return null;
        }

        static public FilePath? ToEntity(this FilePathDto? item)
        {
            if (item != null)
            {
                return new FilePath
                {
                    Id = item.Id,
                    Path = item.Path,
                    PersonId = item.PersonId,
                    Name = item.Name,
                    ClassNumber = item.ClassNumber,
                    ItemNumber = item.ItemNumber,
                    TypeId = item.TypeId
                };
            }
            return null;
        }
    }
}

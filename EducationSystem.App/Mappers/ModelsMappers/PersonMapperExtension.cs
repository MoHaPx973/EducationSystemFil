using EducationSystem.App.Mappers.RolesMappers;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Mappers.ModelsMappers
{
    static public class PersonMapperExtension
    {
        static public PersonDto? ToDto(this Person? item)
        {
            if (item != null)
            {
                return new PersonDto
                {
                    Id = item.Id,
                    Surname = item.Surname,
                    Name = item.Name,
                    MiddleName = item.MiddleName,
                    Phone = item.Phone,
                    Address = item.Address,
                    Gender = item.Gender,
                    Birthday = item.Birthday,
                    Email = item.Email,
                    Role = item.Role.ToDto(),
                   IsHidden= item.IsHidden,
                   DateCreate= item.DateCreate,
                   DateUpdate= item.DateUpdate,
                   DateHidden= item.DateHidden
                };
            }
            return null;
        }

        static public Person? ToEntity(this PersonDto? item)
        {
            if (item != null)
            {
                return new Person
                {
                    Id = item.Id,
                    Surname = item.Surname,
                    Name = item.Name,
                    MiddleName = item.MiddleName,
                    Role = item.Role.ToEntity(),
                    Phone = item.Phone,
                    Address = item.Address,
                    Gender = item.Gender,
                    Birthday = item.Birthday,
                    Email = item.Email,
                    IsHidden = item.IsHidden,
                    DateCreate = item.DateCreate,
                    DateUpdate = item.DateUpdate,
                    DateHidden = item.DateHidden
                };
            }
            return null;
        }
    }
}

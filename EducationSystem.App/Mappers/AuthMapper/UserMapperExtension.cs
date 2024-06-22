using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.App.Mappers.RolesMappers;
using EducationSystem.Domain.AuthModels;
using EducationSystem.Shared.Auth;

namespace EducationSystem.App.Mappers.AuthMapper
{
    static public class UserMapperExtension
    {
        static public UserDto? ToDto(this User? item)
        {
            if (item != null)
            {
                return new UserDto
                {
                    Id = item.Id,
                    Login = item.Login,
                    Password = item.Password,
                    Role = item.Role.ToDto(),
                    LinkPerson = item.LinkPerson.ToDto()
                };
            }
            return null;
        }

        static public User? ToEntity(this UserDto? item)
        {
            if (item != null)
            {
                return new User
                {
                    Id = item.Id,
                    Login = item.Login,
                    Password = item.Password,
                    Role = item.Role.ToEntity(),
                    LinkPerson = item.LinkPerson.ToEntity()
                };
            }
            return null;
        }
    }
}

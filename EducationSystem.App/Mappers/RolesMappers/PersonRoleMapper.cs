using EducationSystem.Domain.Role;
using EducationSystem.Shared.Role;


namespace EducationSystem.App.Mappers.RolesMappers
{
    static public class PersonRoleMapper
    {
        static public PersonRoleDto? ToDto(this PersonRole? item)
        {
            if (item != null)
            {
                return new PersonRoleDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                };
            }
            return null;
        }

        static public PersonRole? ToEntity(this PersonRoleDto? item)
        {
            if (item != null)
            {
                return new PersonRole
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                };
            }
            return null;
        }
    }
}

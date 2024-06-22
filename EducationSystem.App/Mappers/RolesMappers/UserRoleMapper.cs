using EducationSystem.Domain.Role;
using EducationSystem.Shared.Role;


namespace EducationSystem.App.Mappers.RolesMappers
{
    static public class UserRoleMapper
    {
        static public UserRoleDto? ToDto(this UserRole? item)
        {
            if (item != null)
            {
                return new UserRoleDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Priority = item.Priority
                };
            }
            return null;
        }

        static public UserRole? ToEntity(this UserRoleDto? item)
        {
            if (item != null)
            {
                return new UserRole
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Priority = item.Priority
                };
            }
            return null;
        }
    }
}

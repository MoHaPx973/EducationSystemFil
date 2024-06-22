using EducationSystem.Shared.Models;
using EducationSystem.Shared.Role;

namespace EducationSystem.Shared.Auth
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoleDto? Role { get; set; } = new();
        public PersonDto? LinkPerson { get; set; } = new();

    }
}

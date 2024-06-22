
namespace EducationSystem.Shared.Role
{
    public class UserRoleDto//:PersonRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
    }
}

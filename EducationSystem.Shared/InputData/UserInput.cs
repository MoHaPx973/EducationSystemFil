
using EducationSystem.Shared.Models;
using EducationSystem.Shared.Role;

namespace EducationSystem.Shared.InputData
{
    public class UserInput
    {
        public string? Login { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int PersonId { get; set; }
    }
}

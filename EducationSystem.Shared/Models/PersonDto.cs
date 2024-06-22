using EducationSystem.Shared.Models.Helper;
using EducationSystem.Shared.Role;

namespace EducationSystem.Shared.Models
{
    public class PersonDto:SharedModel
	{
        public PersonDto()
        {
            IsHidden = false;
        }
        public int Id { get; set; }
        public string? Surname { get; set; } 
        public string? Name { get; set; } 
        public string? MiddleName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime Birthday { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public PersonRoleDto? Role { get; set; } = new();

    }
}

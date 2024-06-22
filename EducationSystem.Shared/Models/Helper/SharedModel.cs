
namespace EducationSystem.Shared.Models.Helper
{
    public class SharedModel
    {
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateHidden { get; set; }
        public bool IsHidden { get; set; } = false;
    }
}

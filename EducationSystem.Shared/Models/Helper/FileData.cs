
namespace EducationSystem.Shared.Models.Helper
{
    public class FileData
    {
        public int? OwnerId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[]? FileBytes { get; set; }
    }
}

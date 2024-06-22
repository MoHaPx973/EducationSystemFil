namespace EducationSystem.Shared.Files
{
	public class FilePathDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PersonId { get; set; }
        public int ClassNumber { get; set; }
        public int ItemNumber { get; set; }
        public int TypeId { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}

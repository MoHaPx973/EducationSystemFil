namespace EducationSystem.Domain.Files
{
	public class FilePath
	{
		public int Id { get; set; }
		public string Name { get; set; }=string.Empty; // Метод рек 4 класс
        public int PersonId { get; set; } // 2
        public int ClassNumber { get; set; } // 4
        public int ItemNumber { get; set; } // 2
        public int TypeId { get; set; } // 1 (УМК)
        public string Path { get; set; } = string.Empty; // teacher
	}
}

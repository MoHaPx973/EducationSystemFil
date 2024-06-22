namespace EducationSystem.Domain.Other
{
    public class NewsData
    {
        public NewsData()
        {

        }
        public NewsData(string title, string text, string path)
        {
            Title = title;
            Text = text;
            Path = path;
        }

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
}

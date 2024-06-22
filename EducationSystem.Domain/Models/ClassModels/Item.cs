using EducationSystem.Domain.Models.Helper;

namespace EducationSystem.Domain.Models.ClassModels
{
    public class Item:DbModel
    {
        public Item() { }
        public Item(string name, string? description)
        {
            Name = name;
            Description = description;
        }
        public int Id { get; set; }
        public string Name 
        {
            get => _name;
            set
            {
                if (!string.IsNullOrWhiteSpace(_name) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Ошибка заполнения названия");
                }
                _name = value;
            }
        }
        public string? Description 
        {
            get => _description;
            set
            {
                if (!string.IsNullOrWhiteSpace(_description) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                _description = value;
            }
        }
        private string _name=string.Empty;
        private string? _description;
    }
}

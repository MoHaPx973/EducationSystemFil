
namespace EducationSystem.Domain.Models.SсheduleModels
{
    public class Classroom
    {
        public Classroom() { }
        public Classroom(int number, string? description)
        {
            Number = number;
            Description = description;
        }
        public int Number
        {
            get => _number;
            set
            {
                if ((_number != 0) && (value == 0))
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка при записи номера");
                }
                _number = value;
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
        private int _number;
        private string? _description;
    }
}

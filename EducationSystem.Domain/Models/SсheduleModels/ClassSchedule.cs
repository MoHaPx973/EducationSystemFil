using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Models.Helper;

namespace EducationSystem.Domain.Models.SсheduleModels
{
    public class ClassSchedule:DbModel
    {
        public ClassSchedule() { }  
        public ClassSchedule(int number, string? description, SchoolClass linkClass)
        {
            Number = number;
            Description = description;
            LinkClass = linkClass;
        }
        public int Id { get; set; }
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
                    throw new NullReferenceException("Ошибка записи номера");
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
        public SchoolClass LinkClass
        {
            get => _linkClass;
            set
            {
                if ((_linkClass != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки класса");
                }
                _linkClass = value;
            }
        }

        private int _number;
        private string? _description;
        private SchoolClass _linkClass = new();

        
    }
}

using EducationSystem.Domain.Models.Helper;

namespace EducationSystem.Domain.Models.ClassModels
{
    public class Curriculum:DbModel
    {
        public Curriculum() { }
        public Curriculum(int number, int yearFormation, int systemTeaching, string? description)
        {
            Number = number;
            YearFormation = yearFormation;
            SystemTeaching = systemTeaching;
            Description = description;
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
                    throw new NullReferenceException("Ошибка при записи номера");
                }
                _number = value;
            }
        }
        public int YearFormation
        {
            get => _yearFormation;
            set
            {
                if ((_yearFormation != 0) && (value == 0))
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка при записи года формирования");
                }
                _yearFormation = value;
            }
        }

        //количество семестров в году
        public int SystemTeaching
        {
            get => _systemTeaching;
            set
            {
                if ((_systemTeaching != 0) && (value == 0))
                {
                    return;
                }
                if ((value>=5)||(value<=1))
                {
                    throw new NullReferenceException("Ошибка при записи типа системы обучения, выход за границы свойства systemTeaching ");
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка при записи типа системы обучения");
                }
                _systemTeaching = value;
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
        private int _yearFormation;
        private int _systemTeaching;
        private string? _description;


    }
}

using EducationSystem.Domain.Models.Helper;

namespace EducationSystem.Domain.Models.ClassModels
{
    public class SchoolClass:DbModel
    {
        public SchoolClass() { }

        public SchoolClass(int number, string letter, int yearFormation, Curriculum linkCurriculum)
        {
            Number = number;
            Letter = letter;
            YearFormation = yearFormation;
            LinkCurriculum = linkCurriculum;
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
        public string Letter 
        {
            get => _letter;
            set
            {
                if (!string.IsNullOrWhiteSpace(_letter) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Ошибка заполнения литеры");
                }
                _letter = value;
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
        public Curriculum LinkCurriculum 
        {
            get => _linkCurriculum;
            set
            {
                if ((_linkCurriculum != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки учебного плана");
                }
                _linkCurriculum = value;
            }
        }
        
        private int _number;
        private string _letter=string.Empty;
        private int _yearFormation;
        private Curriculum _linkCurriculum=new();
    }
}

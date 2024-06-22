
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Relationships;

namespace EducationSystem.Domain.Models.Helper
{
    public class Assessment
    {
        public int Id { get; set; }
        public Person Student
        {
            get => _student;
            set
            {
                if ((_student != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки ученика");
                }
                _student = value;
            }
        }
        public Person Teacher
        {
            get => _teacher;
            set
            {
                if ((_teacher != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки учителя");
                }
                _teacher = value;
            }
        }
        public SchoolClass StudentClass
        {
            get => _schoolClass;
            set
            {
                if ((_schoolClass != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки класса");
                }
                _schoolClass = value;
            }
        }
        public ItemInCurriculum LinkItem
        {
            get => _item;
            set
            {
                if ((_item != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки предмета");
                }
                _item = value;
            }
        }
        public int Point
        {
            get => _point;
            set
            {
                if ((_point != 0) && (value == 0))
                {
                    return;
                }
                if ((value == 0)||(value>5)||(value<2))
                {
                    throw new NullReferenceException("Ошибка выставления оценки");
                }
                _point = value;
            }
        }
        public int SystemTeachingNumber
        {
            get => _systemTeachingNumber;
            set
            {
                if ((_systemTeachingNumber != 0) && (value == 0))
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка заполнения учебного цикла");
                }
                _systemTeachingNumber = value;
            }
        }
        public string Description
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

        private Person _student=new();
        private Person _teacher=new();
        private SchoolClass _schoolClass=new();
        private ItemInCurriculum _item=new();
        private int _point;
        private int _systemTeachingNumber;
        private string _description = string.Empty;
    }
}

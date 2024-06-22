
namespace EducationSystem.Domain.Models.SсheduleModels
{
    public  class LessonTime
    {
        public LessonTime() { }

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
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (!string.IsNullOrWhiteSpace(_startTime) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Ошибка заполнения время начала урока");
                }
                _startTime = value;
            }
        }
        public string EndTime
        {
            get => _endTime;
            set
            {
                if (!string.IsNullOrWhiteSpace(_endTime) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Ошибка заполнения время окончания урока");
                }
                _endTime = value;
            }
        }

        private int _number;
        public string _startTime=string.Empty;
        public string _endTime=string.Empty;
    }
}

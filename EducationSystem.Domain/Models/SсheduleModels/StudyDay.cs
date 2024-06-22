
namespace EducationSystem.Domain.Models.SсheduleModels
{
    public class StudyDay
    {
        public StudyDay() { }
        public StudyDay(DateTime date, int dayWeekNumber, int weekNumber)
        {
            Date = date;
            DayWeekNumber = dayWeekNumber;
            WeekNumber = weekNumber;
            switch (DayWeekNumber) 
            {
                case (1):DayWeekName = "Понедельник";break;
                case (2): DayWeekName = "Вторник"; break;
                case (3): DayWeekName = "Среда"; break;
                case (4): DayWeekName = "Четверг"; break;
                case (5): DayWeekName = "Пятница"; break;
                case (6): DayWeekName = "Суббота"; break;
            }
        }
        public int Id { get; set; }
        public DateTime Date
        {
            get => _date;
            set
            {
                if ((_date != new DateTime()) && (value == new DateTime()))
                {
                    return;
                }
                if (value == new DateTime())
                {
                    throw new NullReferenceException("Ошибка при записи даты");
                }
                _date = value;
            }
        }
        public int DayWeekNumber
        {
            get => _dayWeekNumber;
            set
            {
                if ((_dayWeekNumber != 0) && (value == 0))
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка при записи номера дня");
                }
                _dayWeekNumber = value;
            }
        }
        public string DayWeekName { get; set; } = string.Empty;
        public int WeekNumber
        {
            get => weekNumber;
            set
            {
                if ((weekNumber != 0) && (value == 0))
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка при записи номера недели");
                }
                weekNumber = value;
            }
        }

        private int weekNumber;
        private DateTime _date;
        private int _dayWeekNumber;
    }
}

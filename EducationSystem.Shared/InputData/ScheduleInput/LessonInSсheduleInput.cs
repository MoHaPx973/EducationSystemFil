
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.Models;

namespace EducationSystem.Shared.InputData.ScheduleInput
{
    public class LessonInSсheduleInput
    {
        public int ItemId { get; set; } 
        public int TeacherId { get; set; }
        public int ClassScheduleId { get; set; } 
        public int RoomNumberId { get; set; } 
        public int TimeId { get; set; }
        public DateTime Day
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


        private DateTime _date;
    }
}

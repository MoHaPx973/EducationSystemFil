using EducationSystem.Shared.Models.ClassDto;

namespace EducationSystem.Shared.Models.ScheduleDto
{
    public class LessonInSсheduleDto
    {
        public int Id { get; set; }
        public ItemDto LinkItem { get; set; } = new ();
        public PersonDto Teacher { get; set; } = new();
        public ClassScheduleDto LinkSchedule { get; set; } = new();
        public ClassroomDto RoomNumber { get; set; } = new();
        public LessonTimeDto Time { get; set; } = new();
        public StudyDayDto Day { get; set; } = new();
    }
}

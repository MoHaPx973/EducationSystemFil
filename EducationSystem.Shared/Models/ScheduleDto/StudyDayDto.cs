
namespace EducationSystem.Shared.Models.ScheduleDto
{
    public class StudyDayDto
    {
		public int Id { get; set; }
		public DateTime Date { get; set; }
        public int DayWeekNumber { get; set; }
        public string DayWeekName { get; set; } = string.Empty;
        public int WeekNumber { get; set; }
    }
}

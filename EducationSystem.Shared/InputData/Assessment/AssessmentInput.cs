
namespace EducationSystem.Shared.InputData.Assessment
{
    public class AssessmentInput
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int ItemId { get; set; }
        public int Point { get; set; }
        public int LessonId { get; set; }
        public int SystemTeachingNumber { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

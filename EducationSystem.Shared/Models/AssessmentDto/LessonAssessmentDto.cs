
using EducationSystem.Shared.Models.Helper;
using EducationSystem.Shared.Models.ScheduleDto;

namespace EducationSystem.Shared.Models.AssessmentDto
{
    public class LessonAssessmentDto: BaseAssessmentDto
    {
        public LessonInSсheduleDto? Lesson { get; set; }
    }
}

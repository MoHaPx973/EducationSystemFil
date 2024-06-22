
using EducationSystem.Domain.Models.AssessmentModels;

namespace EducationSystem.App.Storage.ModelsInterfaces.AssessmentInterfaces
{
    public interface ILessonAssessmentRepository
    {
        IEnumerable<LessonAssessment> GetAllEnumerable();
        IEnumerable<LessonAssessment> GetAllEnumerableByStudentId(int studentId);
        Task<LessonAssessment?> GetByIdAsync(int id);
    }
}

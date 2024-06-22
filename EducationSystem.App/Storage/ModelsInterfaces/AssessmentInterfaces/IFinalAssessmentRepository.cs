
using EducationSystem.Domain.Models.AssessmentModels;

namespace EducationSystem.App.Storage.ModelsInterfaces.AssessmentInterfaces
{
    public interface IFinalAssessmentRepository
    {
        FinalAssessment GetById(int id);
        IEnumerable<FinalAssessment> GetAllEnumerable();
        IEnumerable<FinalAssessment> GetAllEnumerableByStudentId(int studentId);
        IEnumerable<FinalAssessment> GetAllEnumerableByStudentIdByClassId(int studentId,int classId);
        Task<FinalAssessment?> GetByIdAsync(int id);
    }
}

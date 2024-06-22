using EducationSystem.Domain.Relationships;

namespace EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces
{
    public interface IStudentInClassRepository
    {
        IEnumerable<StudentInClass> GetAllEnumerable(bool isStuding);

        IEnumerable<StudentInClass> GetByStudentIdAsync(int studentId);
        IEnumerable<StudentInClass> GetByClassIdAsync(int classId);
        StudentInClass GetOneByStudentIdClassId(int studentId, int classId);
    }
}

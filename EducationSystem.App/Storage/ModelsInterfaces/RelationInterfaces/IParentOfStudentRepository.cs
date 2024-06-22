using EducationSystem.Domain.Relationships;

namespace EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces
{
    public interface IParentOfStudentRepository
    {
        public ParentsOfStudents GetOneByParentIdStudentId(int parentId, int studentId);
        public IEnumerable<ParentsOfStudents> GetAllEnumerable();
        public IEnumerable<ParentsOfStudents> GetByStudentIdAsync(int studentId);
        public IEnumerable<ParentsOfStudents> GetByParentIdAsync(int parentId);
    }
}

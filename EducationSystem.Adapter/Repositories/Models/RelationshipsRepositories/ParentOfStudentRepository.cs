using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.Domain.Relationships;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Models.RelationshipsRepositories
{
    public class ParentOfStudentRepository: IParentOfStudentRepository
    {
        protected EducationDbContext _context;
        public ParentOfStudentRepository(EducationDbContext context)
        {
            _context = context;
        }
        public ParentsOfStudents GetOneByParentIdStudentId(int parentId, int studentId)
        {
            return _context.RelationParentsOfStudents.Include(s => s.Student).Include(c => c.Parent).Include(r => r.Parent.Role).Include(s => s.Student).Include(r => r.Student.Role).Where(p=>p.Parent.Id==parentId).FirstOrDefault(s=>s.Student.Id==studentId);
        }
        public IEnumerable<ParentsOfStudents> GetAllEnumerable()
        {
            return _context.RelationParentsOfStudents.Include(s => s.Student).Include(c => c.Parent).Include(r => r.Parent.Role).Include(s => s.Student).Include(r => r.Student.Role).ToList();
        }

        public IEnumerable<ParentsOfStudents> GetByStudentIdAsync(int studentId)
        {
            return _context.RelationParentsOfStudents.Include(s => s.Student).Include(c => c.Parent).Include(r => r.Parent.Role).Include(s => s.Student).Include(r => r.Student.Role).Where(i=>i.Student.Id==studentId).ToList();
        }
        public IEnumerable<ParentsOfStudents> GetByParentIdAsync(int parentId)
        {
            return _context.RelationParentsOfStudents.Include(s => s.Student).Include(c => c.Parent).Include(r => r.Parent.Role).Include(s => s.Student).Include(r => r.Student.Role).Where(i => i.Parent.Id == parentId).ToList();
        }
    }
}

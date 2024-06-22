using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.Domain.Relationships;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Models.RelationshipsRepositories
{
    public class StudentInClassRepository: IStudentInClassRepository
    {
        protected EducationDbContext _context;
        public StudentInClassRepository(EducationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<StudentInClass> GetAllEnumerable(bool isStuding)
        {
            return _context.RelationStudentsInClasses.Include(s => s.Student).Include(c => c.LinkClass).Include(r => r.Student.Role).Include(cur => cur.LinkClass.LinkCurriculum).Where(st => st.IsStuding == isStuding).ToList();
        }

        public IEnumerable<StudentInClass> GetByStudentIdAsync(int studentId)
        {
            return _context.RelationStudentsInClasses.Include(s => s.Student).Include(c => c.LinkClass).Include(r=>r.Student.Role).Include(cur=>cur.LinkClass.LinkCurriculum).Where(s=>s.StudentId==studentId).ToList();
        }
        public IEnumerable<StudentInClass> GetByClassIdAsync(int classId)
        {
            return _context.RelationStudentsInClasses.Include(s => s.Student).Include(c => c.LinkClass).Include(r => r.Student.Role).Include(cur => cur.LinkClass.LinkCurriculum).Where(c => c.ClassId == classId).Where(st=>st.IsStuding==true).ToList();
        }
        public StudentInClass GetOneByStudentIdClassId(int studentId, int classId)
        {
            return _context.RelationStudentsInClasses.Include(s => s.Student).Include(c => c.LinkClass).Include(r => r.Student.Role).Include(cur => cur.LinkClass.LinkCurriculum).Where(s=>s.StudentId==studentId).FirstOrDefaultAsync(c=>c.ClassId==classId).Result;
        }
    }
}

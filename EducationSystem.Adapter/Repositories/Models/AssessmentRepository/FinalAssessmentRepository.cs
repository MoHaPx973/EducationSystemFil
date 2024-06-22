using EducationSystem.App.Storage.ModelsInterfaces.AssessmentInterfaces;
using EducationSystem.Domain.Models.AssessmentModels;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Models.AssessmentRepository
{
    public class FinalAssessmentRepository:IFinalAssessmentRepository
    {
        protected EducationDbContext _context;
        public FinalAssessmentRepository(EducationDbContext context)
        {
            _context = context;
        }
        public FinalAssessment GetById(int id)
        {
            return _context.FinalAssessments.Include(r => r.Student).Include(r => r.Teacher).Include(r => r.StudentClass).Include(r => r.StudentClass.LinkCurriculum).
                Include(r => r.Teacher.Role).Include(r => r.Student.Role).Include(r => r.LinkItem).Include(r => r.LinkItem.LinkItem).FirstOrDefault(i=>i.Id==id);
        }
        public IEnumerable<FinalAssessment> GetAllEnumerable()
        {
            return _context.FinalAssessments.Include(r => r.Student).Include(r => r.Teacher).Include(r => r.StudentClass).Include(r => r.StudentClass.LinkCurriculum).
                Include(r => r.Teacher.Role).Include(r => r.Student.Role).Include(r=>r.LinkItem).Include(r => r.LinkItem.LinkItem).ToList();
        }
        public IEnumerable<FinalAssessment> GetAllEnumerableByStudentId(int studentId)
        {
            return _context.FinalAssessments.Include(r => r.Student).Include(r => r.Teacher).Include(r => r.StudentClass).Include(r => r.StudentClass.LinkCurriculum).
                Include(r => r.Teacher.Role).Include(r => r.Student.Role).Include(r => r.LinkItem).Include(r => r.LinkItem.LinkItem).Where(s=>s.Student.Id==studentId).ToList();
        }
        public IEnumerable<FinalAssessment> GetAllEnumerableByStudentIdByClassId(int studentId,int classId)
        {
            return _context.FinalAssessments.Include(r => r.Student).Include(r => r.Teacher).Include(r => r.StudentClass).Include(r => r.StudentClass.LinkCurriculum).
                Include(r => r.Teacher.Role).Include(r => r.Student.Role).Include(r => r.LinkItem).Include(r => r.LinkItem.LinkItem).Where(s => s.Student.Id == studentId).Where(c=>c.StudentClass.Id==classId).ToList();
        }

        public async Task<FinalAssessment?> GetByIdAsync(int id)
        {
            return await _context.FinalAssessments.Include(r => r.Student).Include(r => r.Teacher).Include(r => r.StudentClass).Include(r => r.StudentClass.LinkCurriculum).
                Include(r => r.Teacher.Role).Include(r => r.Student.Role).Include(r => r.LinkItem).Include(r => r.LinkItem.LinkItem).FirstOrDefaultAsync(i => i.Id == id);
        }

    }
}

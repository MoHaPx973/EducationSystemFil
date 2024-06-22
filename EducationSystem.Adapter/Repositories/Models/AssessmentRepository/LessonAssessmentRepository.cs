
using EducationSystem.App.Storage.ModelsInterfaces.AssessmentInterfaces;
using EducationSystem.Domain.Models.AssessmentModels;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Models.AssessmentRepository
{
    public class LessonAssessmentRepository:ILessonAssessmentRepository
    {
        protected EducationDbContext _context;
        public LessonAssessmentRepository(EducationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<LessonAssessment> GetAllEnumerable()
        {
            return _context.LessonAssessments.Include(r => r.Student).Include(r => r.Teacher).Include(r => r.StudentClass).Include(r => r.StudentClass.LinkCurriculum).
                Include(r => r.Teacher.Role).Include(r => r.Student.Role).Include(r => r.Lesson).Include(r => r.Lesson.LinkItem).Include(r => r.Lesson.Teacher).Include(r => r.Lesson.LinkSchedule)
                .Include(r => r.Lesson.RoomNumber).Include(r => r.Lesson.Time).Include(r => r.Lesson.Day).ToList();
        }
        public IEnumerable<LessonAssessment> GetAllEnumerableByStudentId(int studentId)
        {
            return _context.LessonAssessments.Include(r => r.Student).Include(r => r.Teacher).Include(r => r.StudentClass).Include(r => r.StudentClass.LinkCurriculum).
                Include(r => r.Teacher.Role).Include(r => r.Student.Role).Include(r => r.Lesson).Include(r => r.Lesson.LinkItem).Include(r => r.Lesson.Teacher).Include(r => r.Lesson.LinkSchedule)
                .Include(r => r.Lesson.RoomNumber).Include(r => r.Lesson.Time).Include(r => r.Lesson.Day).Where(s => s.Student.Id == studentId).ToList();
        }

        public async Task<LessonAssessment?> GetByIdAsync(int id)
        {
            return await _context.LessonAssessments.Include(r => r.Student).Include(r => r.Teacher).Include(r => r.StudentClass).Include(r => r.StudentClass.LinkCurriculum).
                Include(r => r.Teacher.Role).Include(r => r.Student.Role).Include(r=>r.Lesson).Include(r => r.Lesson.LinkItem).Include(r => r.Lesson.Teacher).Include(r => r.Lesson.LinkSchedule)
                .Include(r => r.Lesson.RoomNumber).Include(r => r.Lesson.Time).Include(r => r.Lesson.Day).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}

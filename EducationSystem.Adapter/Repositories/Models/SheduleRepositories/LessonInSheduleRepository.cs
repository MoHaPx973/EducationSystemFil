using EducationSystem.App.Storage.ModelsInterfaces.SheduleInterfaces;
using EducationSystem.Domain.Models.SсheduleModels;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Models.SheduleRepositories
{
    public class LessonInSheduleRepository: ILessonInSheduleRepository
    {
        protected EducationDbContext _context;
        public LessonInSheduleRepository(EducationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<LessonInSсhedule> GetAllEnumerable()
        {
            return _context.LessonsInShedule.Include(r => r.LinkItem).Include(r => r.Teacher).Include(r => r.Teacher.Role).Include(r => r.LinkSchedule).Include(r => r.LinkSchedule.LinkClass).Include(r => r.LinkSchedule.LinkClass.LinkCurriculum).Include(r => r.Day).
                Include(r => r.RoomNumber).Include(r => r.Time).ToList();
        }

        public async Task<LessonInSсhedule?> GetByIdAsync(int id)
        {
            return await _context.LessonsInShedule.Include(r => r.LinkItem).Include(r => r.Teacher).Include(r => r.Teacher.Role).Include(r => r.LinkSchedule).Include(r => r.LinkSchedule.LinkClass).Include(r => r.LinkSchedule.LinkClass.LinkCurriculum).Include(r => r.Day).
                Include(r => r.RoomNumber).Include(r => r.Time).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}

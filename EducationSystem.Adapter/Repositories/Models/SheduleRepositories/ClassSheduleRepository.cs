using EducationSystem.App.Storage.ModelsInterfaces.SheduleInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.SсheduleModels;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Models.SheduleRepositories
{
    public class ClassSheduleRepository: IClassSheduleRepository
    {
        protected EducationDbContext _context;
        public ClassSheduleRepository(EducationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ClassSchedule> GetAllEnumerable()
        {
            return _context.Shedules.Include(r => r.LinkClass).Include(c=>c.LinkClass.LinkCurriculum).ToList();
        }

        public async Task<ClassSchedule?> GetByIdAsync(int id)
        {
            return await _context.Shedules.Include(r => r.LinkClass).Include(c => c.LinkClass.LinkCurriculum).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}

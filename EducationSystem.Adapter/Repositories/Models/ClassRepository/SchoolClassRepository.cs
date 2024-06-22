using EducationSystem.App.Storage.ModelsInterfaces.ClassInterfaces;
using EducationSystem.Domain.Models.ClassModels;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Models.ClassRepository
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        protected EducationDbContext _context;

        public SchoolClassRepository(EducationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SchoolClass> GetAllEnumerable()
        {
            return _context.SchoolClasses.Include(r => r.LinkCurriculum).ToList();
        }

        public async Task<SchoolClass?> GetByIdAsync(int id)
        {
            return await _context.SchoolClasses.Include(r => r.LinkCurriculum).FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}

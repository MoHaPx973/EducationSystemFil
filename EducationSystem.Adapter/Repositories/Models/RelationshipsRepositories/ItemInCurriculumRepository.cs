using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.Domain.Relationships;
using Microsoft.EntityFrameworkCore;


namespace EducationSystem.Adapter.Repositories.Models.RelationshipsRepositories
{
    public class ItemInCurriculumRepository:IItemInCurriculumRepository
    {
        protected EducationDbContext _context;
        public ItemInCurriculumRepository(EducationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ItemInCurriculum> GetAllEnumerable()
        {
            return _context.RelationItemsInCurriculums.Include(s => s.LinkItem).Include(c => c.LinkCurriculum).ToList();
        }

        public IEnumerable<ItemInCurriculum> GetByCurriculumIdAsync(int curriculumId)
        {
            return _context.RelationItemsInCurriculums.Include(s => s.LinkItem).Include(c => c.LinkCurriculum).Where(s => s.CurriculumId == curriculumId).ToList();
        }
        public ItemInCurriculum GetOneByItemIdCurriculumId(int itemId, int curriculumId)
        {
            return _context.RelationItemsInCurriculums.Include(s => s.LinkItem).Include(c => c.LinkCurriculum).Where(s => s.CurriculumId == curriculumId).FirstOrDefaultAsync(s => s.ItemId == itemId).Result;
        }
    }
}

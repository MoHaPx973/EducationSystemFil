using EducationSystem.Domain.Relationships;

namespace EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces
{
    public interface IItemInCurriculumRepository
    {
        public IEnumerable<ItemInCurriculum> GetAllEnumerable();
        public IEnumerable<ItemInCurriculum> GetByCurriculumIdAsync(int curriculumId);
        public ItemInCurriculum GetOneByItemIdCurriculumId(int itemId, int curriculumId);
    }
}

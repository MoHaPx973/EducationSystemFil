using EducationSystem.Domain.Models.ClassModels;

namespace EducationSystem.App.Storage.ModelsInterfaces.ClassInterfaces
{
    public interface ISchoolClassRepository
    {
        Task<SchoolClass?> GetByIdAsync(int id);
        IEnumerable<SchoolClass> GetAllEnumerable();
    }
}

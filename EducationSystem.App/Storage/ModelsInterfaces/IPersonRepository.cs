using EducationSystem.Domain.Models;

namespace EducationSystem.App.Storage.ModelsInterfaces
{
    public interface IPersonRepository
    {
        Task<Person?> GetByIdAsync(int id);
        IEnumerable<Person> GetAllEnumerable();
    }
}

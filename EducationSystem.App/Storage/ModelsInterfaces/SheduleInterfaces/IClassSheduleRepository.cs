using EducationSystem.Domain.Models.SсheduleModels;

namespace EducationSystem.App.Storage.ModelsInterfaces.SheduleInterfaces
{
    public interface IClassSheduleRepository
    {
        IEnumerable<ClassSchedule> GetAllEnumerable();

        Task<ClassSchedule?> GetByIdAsync(int id);
    }
}

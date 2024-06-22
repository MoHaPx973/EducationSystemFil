using EducationSystem.Domain.Models.SсheduleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.App.Storage.ModelsInterfaces.SheduleInterfaces
{
    public interface ILessonInSheduleRepository
    {
        public IEnumerable<LessonInSсhedule> GetAllEnumerable();
        Task<LessonInSсhedule?> GetByIdAsync(int id);
    }
}

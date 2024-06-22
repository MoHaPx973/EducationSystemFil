using EducationSystem.App.Storage.ModelsInterfaces;
using EducationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Models
{
    public class PersonRepository: IPersonRepository
    {
        protected EducationDbContext _context;
        public PersonRepository(EducationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Person> GetAllEnumerable()
        {
            return _context.Persons.Include(r => r.Role).ToList();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Persons.Include(r=>r.Role).FirstOrDefaultAsync(i=>i.Id==id);
        }
    }
}

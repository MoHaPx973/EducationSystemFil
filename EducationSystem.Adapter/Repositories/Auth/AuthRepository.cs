using EducationSystem.Adapter.Repositories.Generic;
using EducationSystem.App.Storage.AuthInterfaces;
using EducationSystem.Domain.AuthModels;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repositories.Auth
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        public AuthRepository(EducationDbContext context) : base(context) { }

        public async Task<User?> GetByLogin(string login)
        {
            return await _context.Users.Include(p => p.LinkPerson).Include(r=>r.Role).Include(pr=>pr.LinkPerson.Role).FirstOrDefaultAsync(x => x.Login == login);
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(p => p.LinkPerson).Include(r => r.Role).Include(pr => pr.LinkPerson.Role).FirstOrDefaultAsync(i => i.Id == id);
        }

        public IEnumerable<User> GetAllEnumerable()
        {
            return _context.Users.Include(p => p.LinkPerson).Include(r => r.Role).Include(pr => pr.LinkPerson.Role).ToList();
        }
    }
}

namespace EducationSystem.Adapter.Repositories.Files
{
    public class FileRepository
    {
        protected EducationDbContext _context;
        public FileRepository(EducationDbContext context)
        {
            _context = context;
        }
        
    }
}

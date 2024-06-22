using EducationSystem.App.Storage.GenericInterfaces;

namespace EducationSystem.Adapter.Repositories.Generic
{
	public class GenericRepository<TEntity>:IGenericRepository<TEntity>
		where TEntity : class
	{
		protected EducationDbContext _context;
		public GenericRepository(EducationDbContext context)
		{ 
			this._context = context; 
		}

		public  List<TEntity> GetAllAsyncWithoutLink()
		{
			return  _context.Set<TEntity>().ToList();
		}

		public async Task<TEntity?> GetByIdAsyncWithoutLink(int id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}

        public async Task<TEntity?> GetByDateAsyncWithoutLink(DateTime date)
        {
            return await _context.Set<TEntity>().FindAsync(date);
        }

        public IEnumerable<TEntity> GetAllEnumerableWithoutLink()
		{
			return _context.Set<TEntity>();
		}

		public void Insert(TEntity entity)
		{
			_context.Set<TEntity>().Add(entity);
		}

		public void Update(TEntity entity)
		{
			_context.Update(entity);
		}
		public void DeleteWithoutLink(TEntity entity)
		{
			_context.Remove(entity);
		}
		public async Task<TEntity?> CombinedKey(int firstId, int secondId)
		{
			return await _context.Set<TEntity>().FindAsync(firstId,secondId);
		}
	}
}


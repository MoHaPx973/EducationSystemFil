using EducationSystem.App.Storage.GenericInterfaces;

namespace EducationSystem.Adapter.Repositories.Generic
{
	public class UnitWork:IUnitWork
	{
		private readonly EducationDbContext _context;
		public UnitWork(EducationDbContext context)
		{
			this._context = context;
		}
		public Task Commit()
		{
			return _context.SaveChangesAsync();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}

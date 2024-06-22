
namespace EducationSystem.App.Storage.GenericInterfaces
{
	public interface IGenericRepository<TEntity>
	{
        Task<TEntity?> GetByIdAsyncWithoutLink(int id);
		Task<TEntity?> GetByDateAsyncWithoutLink(DateTime date);
        List<TEntity> GetAllAsyncWithoutLink();
		IEnumerable<TEntity> GetAllEnumerableWithoutLink();

		void Insert(TEntity entity);

		void Update(TEntity entity);

		void DeleteWithoutLink(TEntity entity);

		Task<TEntity?> CombinedKey(int firstId, int secondId);
	}
}

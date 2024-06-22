
namespace EducationSystem.App.Storage.GenericInterfaces
{
	public interface IUnitWork
	{
		public Task Commit();
		public void Dispose();
	}
}

using EducationSystem.Domain.AuthModels;

namespace EducationSystem.App.Storage.AuthInterfaces
{
	public interface IAuthRepository
	{
		Task<User?> GetByLogin(string login);
		Task<User?> GetByIdAsync(int id);
        IEnumerable<User> GetAllEnumerable();

    }
}

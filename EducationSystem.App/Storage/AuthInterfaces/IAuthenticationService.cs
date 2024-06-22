
namespace EducationSystem.App.Storage.AuthInterfaces
{
	public interface IAuthenticationService
	{
		string? Authenticate(string login, string inputPassword, string storedPassword,string role);
	}
}

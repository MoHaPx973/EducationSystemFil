using EducationSystem.App.Interactor.AuthInteractors;
using EducationSystem.Domain.Role;
using EducationSystem.Shared.Auth;
using EducationSystem.Shared.InputData;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EducationSystem.Api.Controllers.AuthControllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private AuthInteractor _authInteractor;
        public AuthController(AuthInteractor authInteractor)
        {
            _authInteractor = authInteractor;
        }

        [HttpPost("Create")]
        public async Task<Response<UserDto>> Insert(UserInput user)
        {
            return await _authInteractor.Insert(user.Login, user.Password, user.RoleId, user.PersonId);
        }


        [HttpGet("FindById/{id}")]
        public async Task<Response<UserDto>> FindById(int id)
        {
            return await _authInteractor.GetByIdAsync(id);
        }
        [HttpGet("FindByLogin/{login}")]
        public async Task<Response<UserDto>> FindByLogin(string login)
        {
            return await _authInteractor.GetByLoginAsync(login);
        }
        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<UserDto>> GetAllEnumerable()
        {
            return _authInteractor.GetAllEnumerable();

        }
        [HttpGet("AuthenticateAsync")]
        public async Task<Response<string>> AuthenticateAsync(string? login, string? password)
        {
            return await _authInteractor.AuthenticateAsync(login, password);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<UserDto>> Delete(int id)
        {
            return await _authInteractor.Delete(id);
        }
    }
}

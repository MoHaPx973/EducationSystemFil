using EducationSystem.App.Interactor.RoleInteractors;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Role;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.RolesControllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserRoleController : Controller
    {
        private UserRoleInteractor _userRoleInteractor;
        public UserRoleController(UserRoleInteractor userRoleInteractor)
        {
            _userRoleInteractor = userRoleInteractor;
        }

        [HttpPost("FirstCreate")]
        public Response<IEnumerable<UserRoleDto>> FirstCreate()
        {
            return _userRoleInteractor.FirstCreate();
        }
        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<UserRoleDto>> GetAllEnumerable()
        {
            return _userRoleInteractor.GetAllEnumerable();
        }
    }
}

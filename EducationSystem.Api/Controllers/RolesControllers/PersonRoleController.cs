using EducationSystem.App.Interactor.RoleInteractors;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Role;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.RolesControllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonRoleController : Controller
    {
        private PersonRoleInteractor _interactor;
        public PersonRoleController(PersonRoleInteractor interactor)
        {
            _interactor = interactor;
        }

        [HttpPost("FirstCreate")]
        public Response<IEnumerable<PersonRoleDto>> FirstCreate()
        {
            return _interactor.FirstCreate();
        }
        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<PersonRoleDto>> GetAllEnumerable()
        {
            return _interactor.GetAllEnumerable();
        }
    }
}

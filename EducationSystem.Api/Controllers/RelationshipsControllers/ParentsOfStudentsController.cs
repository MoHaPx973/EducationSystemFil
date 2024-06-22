using EducationSystem.App.Interactor.RelationshipsInteractors;
using EducationSystem.Shared.InputData.RelationshipsInput;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Relationships;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.RelationshipsControllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParentsOfStudentsController : Controller
    {
        private ParentOfStudentInteractor _interactor;

        public ParentsOfStudentsController(ParentOfStudentInteractor interactor)
        {
            _interactor = interactor;
        }

        [HttpPost("Create")]
        public async Task<Response<ParentsOfStudentsDto>> Insert(ParentsOfStudentsInput newEntity)
        {
            return await _interactor.Insert(newEntity.ParentId, newEntity.StudentId);
        }
        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<ParentsOfStudentsDto>> GetAllEnumerable()
        {
            return _interactor.GetAllEnumerable();
        }
        [HttpGet("GetByParentIdAsync/{parentId}")]
        public async Task<Response<IEnumerable<ParentsOfStudentsDto>>> GetByParentIdAsync(int parentId)
        {
            return await _interactor.GetByParentIdAsync(parentId);
        }
        [HttpGet("GetByStudentIdAsync/{studentId}")]
        public async Task<Response<IEnumerable<ParentsOfStudentsDto>>> GetByStudentIdAsync(int studentId)
        {
            return await _interactor.GetByStudentIdAsync(studentId);
        }

        [HttpDelete("Delete/{parentId}/{studentId}")]
        public async Task<Response<ParentsOfStudentsDto>> Delete(int parentId, int studentId)
        {
            return await _interactor.Delete(parentId, studentId);
        }
    }
}

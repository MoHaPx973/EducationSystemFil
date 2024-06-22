using EducationSystem.App.Interactor.RelationshipsInteractors;
using EducationSystem.Shared.InputData.RelationshipsInput;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Relationships;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.RelationshipsControllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentInClassController : Controller
    {
        private StudentInClassInteractor _interactor;

        public StudentInClassController(StudentInClassInteractor interactor)
        {
            _interactor = interactor;
        }

        [HttpPost("Create")]
        public async Task<Response<StudentInClassDto>> Insert(StudentInClassInput newEntity)
        {
            return await _interactor.Insert(newEntity.StudentId, newEntity.ClassId);
        }
        [HttpGet("GetAllEnumerable/{isStuding}")]
        public Response<IEnumerable<StudentInClassDto>> GetAllEnumerableAsync(bool isStuding)
        {
            return  _interactor.GetAllEnumerable(isStuding);
        }
        [HttpGet("GetAllEnumerableByStudentId/{studentId}")]
        public async Task<Response<IEnumerable<StudentInClassDto>>> GetAllEnumerableByStudentId(int studentId)
        {
            return await _interactor.GetAllEnumerableByStudentId(studentId);
        }
        [HttpGet("GetAllEnumerableByClassId/{classId}")]
        public async Task<Response<IEnumerable<StudentInClassDto>>> GetAllEnumerableByClassId(int classId)
        {
            return await _interactor.GetAllEnumerableByClassId(classId);
        }
        [HttpDelete("Delete/{studentId}/{classId}")]
        public async Task<Response<StudentInClassDto>> Delete(int studentId, int classId)
        {
            return await _interactor.Delete(studentId,classId);
        }
        [HttpPut("HideOrShow/{studentId}/{classId}")]
        public async Task<Response<StudentInClassDto>> HideOrShow(int studentId, int classId)
        {
            return await _interactor.HideOrShow(studentId,classId);
        }
    }
}

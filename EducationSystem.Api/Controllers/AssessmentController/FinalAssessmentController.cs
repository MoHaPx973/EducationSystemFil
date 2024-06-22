using EducationSystem.App.Interactor.AssessmentInteractor;
using EducationSystem.App.Interactor.AuthInteractors;
using EducationSystem.Shared.InputData.Assessment;
using EducationSystem.Shared.Models.AssessmentDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.AssessmentController
{
    [ApiController]
    [Route("[controller]")]
    public class FinalAssessmentController : Controller
    {
        private FinalAssessmentInteractor _interactor;
        public FinalAssessmentController(FinalAssessmentInteractor interactor)
        {
            _interactor = interactor;
        }
        [HttpPost("Create")]
        public async Task<Response<FinalAssessmentDto>> Insert(AssessmentInput input)
        {
            return await _interactor.Insert(input.StudentId, input.TeacherId,input.ClassId, input.ItemId, input.Point,input.SystemTeachingNumber,input.Description);
        }
        [HttpGet("GetById/{assessmentId}")]
        public Response<FinalAssessmentDto> GetById(int assessmentId)
        {
            return _interactor.GetById(assessmentId);
        }
        [HttpGet("GetByStudentId")]
        public Response<IEnumerable<FinalAssessmentDto>> GetAllByStudentId(int studentId)
        {
            return _interactor.GetAllByStudentId(studentId);
        }
        [HttpGet("GetByStudentIdClassId/{studentId}/{classId}")]
        public Response<IEnumerable<FinalAssessmentDto>> GetByStudentIdClassId(int studentId,int classId)
        {
            return _interactor.GetByStudentIdClassId(studentId, classId);
        }
        [HttpGet("GetByStudentIdClassIdSystemTeachingNumber/{studentId}/{classId}/{systemTeaching}")]
        public Response<IEnumerable<FinalAssessmentDto>> GetByStudentIdClassIdSystemTeachingNumber(int studentId, int classId, int systemTeaching)
        {
            return _interactor.GetByStudentIdClassIdSystemTeachingNumber(studentId, classId, systemTeaching);
        }
    }
}

using EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors;
using EducationSystem.Shared.InputData.ScheduleInput;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.SсheduleControllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomController : Controller
    {
        private ClassroomInteractor _interactor;
        public ClassroomController(ClassroomInteractor interactor)
        {
            _interactor = interactor;
        }
        [HttpPost("Create")]
        public async Task<Response<ClassroomDto>> Insert(ClassroomInput newEntity)
        {
            return await _interactor.Insert(newEntity.Number, newEntity.Description);
        }
        [HttpGet("FindByNumber/{number}")]
        public async Task<Response<ClassroomDto>> Find(int number)
        {
            return await _interactor.GetByIdAsync(number);
        }
        [HttpPut("Update")]
        public async Task<Response<ClassroomDto>> Update(ClassroomInput newData)
        {
            return await _interactor.Update(newData.Number, newData.Description);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<ClassroomDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }
        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<ClassroomDto>> GetAllEnumerable()
        {
            return _interactor.GetAllEnumerable();
        }
        [HttpGet("GetPageEnumerable/{start}/{count}")]
        public Response<IEnumerable<ClassroomDto>> GetPageByEnumerable(int start, int count)
        {
            return _interactor.GetPageEnumerable(start, count);
        }
    }
}

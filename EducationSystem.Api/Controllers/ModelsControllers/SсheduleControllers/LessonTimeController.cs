using EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors;
using EducationSystem.Shared.InputData.ScheduleInput;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.SсheduleControllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonTimeController : Controller
    {
        private LessonTimeInteractor _interactor;
        public LessonTimeController(LessonTimeInteractor interactor)
        {
            _interactor = interactor;
        }
        [HttpPost("Create")]
        public async Task<Response<LessonTimeDto>> Insert(LessonTimeInput newEntity)
        {
            return await _interactor.Insert(newEntity.Number,newEntity.StartTime, newEntity.EndTime);
        }
        [HttpGet("FindByNumber/{id}")]
        public async Task<Response<LessonTimeDto>> Find(int id)
        {
            return await _interactor.GetByIdAsync(id);
        }
        [HttpPut("Update")]
        public async Task<Response<LessonTimeDto>> Update(LessonTimeInput newData)
        {
            return await _interactor.Update(newData.Number, newData.StartTime, newData.EndTime);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<LessonTimeDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }
        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<LessonTimeDto>> GetAllEnumerable()
        {
            return _interactor.GetAllEnumerable();
        }
        [HttpGet("GetPageEnumerable/{start}/{count}")]
        public Response<IEnumerable<LessonTimeDto>> GetPageByEnumerable(int start, int count)
        {
            return _interactor.GetPageEnumerable(start, count);
        }
    }
}

using EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors;
using EducationSystem.Shared.InputData.ScheduleInput;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.SсheduleControllers
{
    [ApiController]
    [Route("[controller]")]
    public class LessonInSсheduleController : Controller
    {
        private LessonInSсheduleInteractor _interactor;

        public LessonInSсheduleController(LessonInSсheduleInteractor interactor)
        {
            _interactor = interactor;
        }

        [HttpPost("Create")]
        public async Task<Response<LessonInSсheduleDto>> Insert(LessonInSсheduleInput newData)
        {
            return await _interactor.Insert(newData.ItemId, newData.TeacherId, newData.ClassScheduleId, newData.RoomNumberId, newData.TimeId, newData.Day);
        }
        [HttpGet("FindById/{id}")]
        public async Task<Response<LessonInSсheduleDto>> GetByIdAsync(int id)
        {
            return await _interactor.GetByIdAsync(id);
        }
        [HttpPut("Update/{id}")]
        public async Task<Response<LessonInSсheduleDto>> Update(int id, LessonInSсheduleInput newData)
        {
            return await _interactor.Update(id, newData.ItemId, newData.TeacherId, newData.ClassScheduleId, newData.RoomNumberId, newData.TimeId, newData.Day);
        }

        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<LessonInSсheduleDto>> GetAllEnumerable()
        {
            return _interactor.GetAllEnumerable();
        }

        [HttpGet("GetAllByScheduleId/{scheduleId}")]
        public Response<IEnumerable<LessonInSсheduleDto>> GetAllByScheduleId(int scheduleId)
        {
            return _interactor.GetAllByScheduleId(scheduleId);
        }
        [HttpGet("GetAllByTeacherId/{teacherId}")]
        public Response<IEnumerable<LessonInSсheduleDto>> GetAllByTeacherId(int teacherId)
        {
            return _interactor.GetAllByTeacherId(teacherId);
        }

        [HttpGet("GetPageEnumerable")]
        public Response<IEnumerable<LessonInSсheduleDto>> GetPageEnumerable(int start, int count)
        {
            return _interactor.GetPageEnumerable(start, count);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<LessonInSсheduleDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }

    }
}

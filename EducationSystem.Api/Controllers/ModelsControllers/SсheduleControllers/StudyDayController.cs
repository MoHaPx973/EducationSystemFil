using EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.SсheduleControllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudyDayController : Controller
    {
        private StudyDayInteractor _interactor;
        public StudyDayController(StudyDayInteractor interactor)
        {
            _interactor = interactor;
        }
        [HttpPost("Create")]
        public async Task<Response<IEnumerable<StudyDayDto>>> Insert(int firstSeptemberDateNumber, bool isLeap, int year)
        {
            return await _interactor.InsertMany(firstSeptemberDateNumber, isLeap, year);
        }
        [HttpGet("GetAll")]
        public async Task<Response<IEnumerable<StudyDayDto>>> GetAll()
        {
            return await _interactor.GetAll();
        }
		[HttpGet("FindById/{id}")]
		public async Task<Response<StudyDayDto>> GetByIdAsync(int id)
		{
			return await _interactor.GetByIdAsync(id);
		}
		[HttpGet("FindByDate/{date}")]
        public async Task<Response<StudyDayDto>> GetByDateAsync(DateTime date)
        {
            return await _interactor.GetByDateAsync(date);
        }
        [HttpDelete("Delete")]
        public async Task<Response<IEnumerable<StudyDayDto>>> Delete(bool isLeap, int year)
        {
            return await _interactor.Delete( isLeap, year);
        }
    }
}

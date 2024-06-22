using EducationSystem.App.Interactor.ModelsInteractors.ClassInteractors;
using EducationSystem.Shared.InputData.ClassInput;
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.ClassControllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurriculumController : Controller
    {
        private CurriculumInteractor _interactor;
        public CurriculumController(CurriculumInteractor interactor)
        {
            _interactor = interactor;
        }
        [HttpPost("Create")]
        public async Task<Response<CurriculumDto>> Insert(CurriculumInput newEntity)
        {
            return await _interactor.Insert(newEntity.Number,newEntity.YearFormation, newEntity.SystemTeaching, newEntity.Description);
        }
        [HttpGet("FindById/{id}")]
        public async Task<Response<CurriculumDto>> Find(int id)
        {
            return await _interactor.GetByIdAsync(id);
        }
        [HttpPut("Update/{id}")]
        public async Task<Response<CurriculumDto>> Update(int id, CurriculumInput newData)
        {
            return await _interactor.Update(id, newData.Number, newData.YearFormation, newData.SystemTeaching, newData.Description);
        }
        [HttpDelete("HideOrShow/{id}")]
        public async Task<Response<CurriculumDto>> HideOrShow(int id)
        {
            return await _interactor.HideOrShow(id);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<CurriculumDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }
        [HttpGet("GetAllEnumerable/{isHidden}")]
        public Response<IEnumerable<CurriculumDto>> GetAllEnumerable(bool isHidden)
        {
            return _interactor.GetAllEnumerable(isHidden);
        }
        [HttpGet("GetPageEnumerable/{isHidden}/{start}/{count}")]
        public Response<IEnumerable<CurriculumDto>> GetPageByEnumerable(bool isHidden, int start, int count)
        {
            return _interactor.GetPageEnumerable(isHidden, start, count);
        }
        [HttpGet("GetPageEnumerableByYear/{year}/{isHidden}/{start}/{count}")]
        public Response<IEnumerable<CurriculumDto>> GetPageEnumerableByYear(int year, bool isHidden, int start, int count)
        {
            return _interactor.GetPageEnumerableByYear(year,isHidden,start,count);
        }
    }
}

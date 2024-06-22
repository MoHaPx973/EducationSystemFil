using EducationSystem.App.Interactor.ModelsInteractors.ClassInteractors;
using EducationSystem.Shared.InputData.ClassInput;
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.ClassControllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolClassController : Controller
    {
        private SchoolClassInteractor _interactor;
        public SchoolClassController(SchoolClassInteractor schoolClassInteractor)
        {
            _interactor = schoolClassInteractor;
        }

        //[HttpPut("{buildingObjectId}/documents/{documentId}")]
        [HttpPost("Create")]
        public async Task<Response<SchoolClassDto>> Insert(SchoolClassInput newEntity)
        {
            return await _interactor.Insert(newEntity.Number,newEntity.Letter,newEntity.YearFormation, newEntity.CurriculumId);
        }

        [HttpGet("FindById/{id}")]
        public async Task<Response<SchoolClassDto>> Find(int id)
        {
            return await _interactor.GetByIdAsync(id);
        }
        [HttpPut("Update/{id}")]
        public async Task<Response<SchoolClassDto>> Update(int id, SchoolClassInput newData)
        {
            return await _interactor.Update(id, newData.Number, newData.Letter, newData.YearFormation, newData.CurriculumId);
        }
        [HttpDelete("HideOrShow/{id}")]
        public async Task<Response<SchoolClassDto>> HideOrShow(int id)
        {
            return await _interactor.HideOrShow(id);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<SchoolClassDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }
        [HttpGet("GetAllEnumerable/{isHidden}")]
        public Response<IEnumerable<SchoolClassDto>> GetAllEnumerable(bool isHidden)
        {
            return _interactor.GetAllEnumerable(isHidden);
        }
        [HttpGet("GetPageEnumerable/{isHidden}/{start}/{count}")]
        public Response<IEnumerable<SchoolClassDto>> GetPageByEnumerable(bool isHidden, int start, int count)
        {
            return _interactor.GetPageEnumerable(isHidden, start, count);
        }

        [HttpGet("GetAllByYear/{year}")]
        public Response<IEnumerable<SchoolClassDto>> GetAllByYear(int year)
        {
            return _interactor.GetAllByYear(year);
        }
    }
}
